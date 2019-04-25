// 总价
var total = document.getElementById("total");
// 发送按钮
var send = document.getElementById("send");
// 座位号
var seatNumber = document.getElementById("seatNumber");

// 获取按钮，并绑定点击事件
var numInputs = document.querySelectorAll("input.input-number")
console.log(numInputs)

// 绑定事件
numInputs.forEach((input)=>{
    input.addEventListener('input', (event)=>{
        input.value = parseInt(input.value);
        total.innerText = getTotal().toString();
    })
})

// 计算总价格
function getTotal(){
    var total = 0;
    
    numInputs.forEach((input)=>{
        var localTotal = parseFloat(input.dataset["itemprice"]) * parseInt(input.value);
        total+= localTotal;
    })
    return total;
}



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

    numInputs.forEach((item) => {
        var count = parseInt(item.value);
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