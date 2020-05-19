
var connection = new signalR.HubConnectionBuilder().withUrl("/DemoHub").build();
var TagPositions = [];
//alert((connection.on));

connection.on("ReceiveMessage", function (user, message) {
    alert("out");
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    alert(encodedMsg);
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("Demo", function (Msg) {

    var ObjMsg = Msg.split("\\");

    var ObjMsgTemp = JSON.parse(ObjMsg[0]);

    var emp = JSON.stringify(ObjMsg[1]);

    var targetGateway = window.sessionStorage.getItem("Gateway");

    //alert(targetGateway);

    //var thi1 = ifObjMsg.dev.filter(function (a) {
    //    return (a.thing == targetGateway);
    //});

    //alert(thi1);

    if (ObjMsgTemp.dev == targetGateway) {
        //("done");
        var vObject = emp.replace("{", "").replace("}", "").replace('"', '').replace('"', '');

        //alert(vObject);

        var _edetails = vObject.split(",");

        var Name = _edetails[0];
        var Des = _edetails[2];
        var Dept = _edetails[1];
        var Mno = _edetails[3];

        var name = Name.split("=");

        var _name = name[1];

        var des = Des.split("=");

        var _des = des[1];

        var dept = Dept.split("=");

        var _dept = dept[1];

        var mno = Mno.split("=");

        var _mno = mno[1];

        $("#name").text(_name);

        $("#des").text(_des);

        $("#dept").text(_dept);

        $("#mno").text(_mno);

        if (ObjMsgTemp.scnItms != null) {
            //alert("b");

            var targetTag = JSON.parse("360");

            //alert(targetTag)

            var thing = ObjMsgTemp.scnItms[0];


            //var thing = ObjMsgTemp.scnItms[0].thing;
            //alert("c");
            //alert(thing);

            var l = thing.scnTime;

            $("#sTime").text(l);
            sessionStorage.setItem("ThingNo", thing.thing);
        }
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});






