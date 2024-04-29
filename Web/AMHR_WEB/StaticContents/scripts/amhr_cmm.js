/*
    amhr_cmm.js
    - javascript 공통 모듈
*/

var MsgBox = {

    /**
     * MsgBox.Alert() : 알림 함수
     * @param {any} msg         // 알림메세지
     * @param {any} okhandler   // Ok 버튼 클릭에 적용할 함수
     */
    Alert: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            $("#msg_popup #btn_comfirm").hide();
            $("#msg_popup #btn_alert").show();

            $("msg_popup #alert_ok").unbind();
            $("msg_popup .model-body").html(msg);
            $("msg_popup").model('show');

            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").model("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { resolve(); }
                else { reject(); }
            });
        }).then(okhandler).catch(function () { });
    },

    Confirm: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            var flag = false;
            $("#msg_popup #btn_alert").hide();
            $("#msg_popup #btn_confirm").show();

            $("#msg_popup #confirm_yes").unbind();
            $("#msg_popup #confirm_no").unbind();
            $("#msg_popup .modal-body").html(msg);
            $("#msg_popup").modal("show");

            $("#msg_popup").on("keypress", function (e) {

                var keycode = (e.keyCode ? e.keyCode : e.which);
                if (keycode == '13')
                {
                    flag = true;
                    $("#msg_popup").modal("hide");
                }
            });

            $("#msg_popup #confirm_yes").click(function () {
                flag = true;
            });

            $("#msg_popup #confirm_no").click(function () {
                flag = false;
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (yeshandler != null && flag == true) { resolve(1); }
                else if (nohandler != null && flag == false) { resolove(2); }
                else { reject(); }
            });
        }).then(function (value) {
            if (value == 1) { yeshandler(); }
            else if (value == 2) { nohandler(); }
        }).catch(function () { });
    }



}