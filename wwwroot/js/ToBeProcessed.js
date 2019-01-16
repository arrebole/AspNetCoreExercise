

var btns = document.querySelectorAll("button");


btns.forEach(element => {
    element.onclick = function () {
        var id = element.dataset.itemid;
        fetch("/management/GoProcess", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: "include",
            body: JSON.stringify({ "Id": id }),
        })
            .then(res => res.json())
            .then(res => {
                if (res.code == "Ok") {
                    window.location.reload()
                } else {
                    alert("失败");
                }
            })
    }
});
