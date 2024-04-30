/*
    amhr_cmm.js
    - javascript 공통 모듈
*/
    var modal_status = {
          status_inform      : "inform"
        , status_warning     : "warning"
        , status_dagner      : "danger"
        , status_confirm     : "confirm"
        , status_alert       : "alert"
    };

    var MsgBox = {

        Alert: function (msg, okhandler) {
                $("#btn_confirm").hide();
                $("#btn_alert").show();

                $("#msg_popup #alert_ok").unbind();
                //$("#msg_popup .modal-body").html(msg);
                setModalDesign(msg, "alert");
                $("#msg_popup").modal('show');

                $("#msg_popup #alert_ok").click(function () {
                    $("#msg_popup").modal("hide");
                });

                $("#msg_popup").on("hidden.bs.modal", function (e) {
                    e.stopPropagation();
                    if (okhandler != null) { }
                    else { }
                });
        },

        Warning: function (msg, okhandler) {
            $("#btn_confirm").hide();
            $("#btn_alert").show();

            $("#msg_popup #alert_ok").unbind();
            //$("#msg_popup .modal-body").html(msg);
            setModalDesign(msg, "warning");
            $("#msg_popup").modal('show');

            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { }
                else { }
            });
        },

        Danger: function (msg, okhandler) {
            $("#btn_confirm").hide();
            $("#btn_alert").show();

            $("#msg_popup #alert_ok").unbind();
            //$("#msg_popup .modal-body").html(msg);
            setModalDesign(msg, "danger");
            $("#msg_popup").modal('show');

            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { }
                else { }
            });
        },

        Inform: function (msg, okhandler) {
            $("#btn_confirm").hide();
            $("#btn_alert").show();

            $("#msg_popup #alert_ok").unbind();
            //$("#msg_popup .modal-body").html(msg);
            setModalDesign(msg, "inform");
            $("#msg_popup").modal('show');

            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { }
                else { }
            });
        },

        Confirm: function (msg, resultFlag) {
                var flag = false;
                $("#btn_alert").hide();
                $("#btn_confirm").show();

                $("#confirm_yes").unbind();
                $("#confirm_no").unbind();
                //$("#msg_popup .modal-body").html(msg);
                setModalDesign(msg, "confirm");
                $("#msg_popup").modal("show");

                $("#msg_popup").on("keypress", function (e) {

                    var keycode = (e.keyCode ? e.keyCode : e.which);
                    if (keycode == '13')
                    {
                        flag = false;
                        resultFlag(flag);
                        $("#msg_popup").modal("hide");
                        e.stopPropagation();
                    }
                });

                $("#confirm_yes").on("click", function (e) {
                    flag = true;
                    resultFlag(flag);
                    $("#msg_popup").modal("hide");
                    e.stopPropagation();
                    
                });
           

                $("#confirm_no").on("click", function (e) {
                    flag = false;
                    resultFlag(flag);
                    $("#msg_popup").modal("hide");
                    e.stopPropagation();
                    
                });
        }
    }

    function setModalDesign(message, status)
    {
        var modal_title = "";
        var modal_message = message;
        var bgColorDesign = "";
        var btnBgColorDesign = "";
        var fontColorDesign = "";
        switch (status)
        {
            case modal_status.status_inform:
                modal_title = "Info";
                bgColorDesign = "bg-info";
                btnBgColorDesign = "btn-info";
                fontColorDesign = "text-white";

                break;
            case modal_status.status_warning:
                modal_title = "Warning";
                bgColorDesign = "bg-warning";
                btnBgColorDesign = "btn-warning";
                fontColorDesign = "";

                break;
            case modal_status.status_dagner:
                modal_title = "Danger";
                bgColorDesign = "bg-danger";
                btnBgColorDesign = "btn-danger";
                fontColorDesign = "text-white";
                break;
            case modal_status.status_confirm:
                modal_title = "Confirm";
                bgColorDesign = "bg-success";
                btnBgColorDesign = "btn-success";
                fontColorDesign = "text-white";
                break;
            case modal_status.status_alert:
                modal_title = "Alert";
                bgColorDesign = "bg-primary";
                btnBgColorDesign = "btn-primary";
                fontColorDesign = "text-white";
                break;
            default:
                modal_title = "Alert";
                bgColorDesign = "bg-primary";
                btnBgColorDesign = "btn-primary";
                fontColorDesign = "text-white";
                break;
        }

        $("#amhr_modal_title").html(modal_title);
        $("#amhr_modal_header").attr("class", "modal-header " + bgColorDesign + " " + fontColorDesign);
        $("#amhr_modal_body_text").html(modal_message);
        $("button[name='amhr_btn_ok']").attr("class", "btn " + btnBgColorDesign);
    }