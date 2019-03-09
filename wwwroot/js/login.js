

var userName = document.getElementById("userName");
var passWord = document.getElementById("passWord");
var btn = document.getElementById("loginBtn");

//为浏览器添加cookie认证
function addCookie(objName, objValue, minute) {

    var str = objName + "=" + objValue;

    var date = new Date();
    var ms = minute * 60 * 1000;
    date.setTime(date.getTime() + ms);
    str += "; expires=" + date.toGMTString();

    str += "; path=/";
    document.cookie = str;
};


btn.onclick = function () {

    // 将登录信息发送给后端
    fetch("/account/login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "userName": userName.value,
            "passWord": passWord.value
        }),
    })
        .then(res => res.json())
        .then(res => {
            // 如果成功则设置cookie,最长有效时间30分钟
            if (res.code != "error") {
                if (document.cookie.replace(/(?:(?:^|.*;\s*)token\s*\=\s*([^;]*).*$)|^.*$/, "$1") != res.token) {
                    addCookie("token", res.token, 30);
                }
                window.location.href = "/";
            } else {
                alert("密码错误")
            }
        })
}

