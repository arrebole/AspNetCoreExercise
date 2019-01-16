// 总价
var total = document.getElementById("total");
// 发送按钮
var send = document.getElementById("send");
// 座位号
var seatNumber = document.getElementById("seatNumber");

// 获取按钮，并绑定点击事件
var btns = document.querySelectorAll("button.btn");
var counts = document.querySelectorAll("th.count");


//
btns.forEach((btn) => {
    // 为每个按钮绑定点击事件
    btn.onclick = function () {

        // 获取按钮的id
        var id = btn.dataset.itemid;
        // 从counts中读取 id对应itemid属性的元素
        for (var i = 0; i < counts.length; i++) {
            if (counts[i].dataset.itemid == id) {

                var old = parseInt(counts[i].innerHTML);

                if (btn.dataset.tohandle == "increase") {
                    counts[i].innerHTML = ++old;
                    total.innerText = parseFloat(total.innerText) + parseFloat(btn.dataset["price"]);
                } else {
                    if (old > 0) {
                        counts[i].innerHTML = --old;
                        total.innerText = parseFloat(total.innerText) - parseFloat(btn.dataset["price"]);
                    }
                }
            }
        }
    }

})

// 提交数据
send.onclick = function () {
    var seat = parseInt(seatNumber.value);
    // 检验数据
    if (isNaN(seat) || seat <= 0) {
        seatNumber.value = "";
        seatNumber.placeholder = "错误的内容";
        return;
    } else if (total.innerText == "0") {
        seatNumber.value = "";
        seatNumber.placeholder = "没有需要提交的内容";
        return;
    }

    // 计算订单
    var menuList = new Array();


    counts.forEach((item) => {

        var count = parseInt(item.innerHTML);
        if (count > 0) {
            var id = parseInt(item.dataset["itemid"]);
            menuList.push({ id, count })
        }
    })

    var message = {
        seat,
        "time": new Date().toLocaleString('chinese', { hour12: false }),
        menuList,
    };

    //


    // 将订单发送给后端
    fetch("/home/ApplyOrder", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        credentials: "include",
        body: JSON.stringify(message),
    })
        .then(res => res.json())
        .then(res => {
            if (res.code == "Ok") {
                location.reload();
            } else {
                $("#exampleModal").modal("hide");
                $('.modal-backdrop').remove();
                alert("服务器发生错误")
            }
        })

}