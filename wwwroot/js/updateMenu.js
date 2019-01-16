


var updateMenuInfo = document.getElementById("updateMenuInfo");
var updateMenuBtn = document.getElementById("updateMenuBtn");
var updateMenuName = document.getElementById("updateMenuName");
var updateMenuGroup = document.getElementById("updateMenuGroup");
var updateMenuPrice = document.getElementById("updateMenuPrice");
var updateMenuFunc = document.getElementById("updateMenuFunc");

updateMenuBtn.onclick = function () {
    var price = parseFloat(updateMenuPrice.value);
    // 没有名字则返回错误
    if (updateMenuName.value == "") {
        updateMenuInfo.innerText = "名称为空";
        return;
    }
    // 价格不是数字则返回错误
    else if (updateMenuFunc.value != "删除" && (isNaN(price) || price < 0)) {
        updateMenuInfo.innerText = "价格错误"
        return;
    }
    var message = {
        "updateMenuGroup": updateMenuGroup.value,
        "updateMenuName": updateMenuName.value,
        "updateMenuPrice": price.toString(),
        "updateMenuFunc": updateMenuFunc.value
    }


    fetch("/management/updateMenuHandle", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        credentials: "include",
        body: JSON.stringify(message),
    })
        .then(res => res.json())
        .then(res => {
            if (res.code = "Ok") {
                updateMenuInfo.innerText = "更新成功";
                updateMenuName.value = "";
                updateMenuPrice.value = "";
            } else {
                updateMenuInfo.innerText = "更新失败";
            }
        })





}