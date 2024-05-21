/*
    amhr_cmm.js
    - javascript 공통 모듈
*/

/**
    * - modal_status
    * - Javascript로 관리하는 modal_status 
    * - inform     - 남색
    * - warning    - 노란색
    * - danger     - 주황색
    * - confirm    - 초록색
    * - alert      - 분홍색(기본)
    */
var modal_status = {
      status_inform      : "inform"
    , status_warning     : "warning"
    , status_dagner      : "danger"
    , status_confirm     : "confirm"
    , status_alert       : "alert"
};

/** 
    * - MsgBox
    * - Alert ~ Confirm Modal Layer Popup
    * - 기본 로직
    *   > Confirm 의 경우 : btn_confirm 을 보여주고 btn_alert 영역을 숨긴다.
    *                     : YES / NO 에 따라 Return받는 flag 가 true / false 로 달라진다.
    * 
    *   > 나머지 의 경우 : btn_confirm 을 숨기고 btn_alert 영역을 보여준다.
    *                    : okhandler - 필요시 custom 함수를 호출할 수 있다.
    * 
    *   > 공통 : setModalDesign(메시지, 상태분류) 함수로 모달 디자인을 바꾸면서 띄운다.
      
        > ex) ==================================================================================
        var confirmFlag = false;
        MsgBox.Confirm("로그인 하시겠습니까??", function resultFlag(flag)
        {
            confirmFlag = flag;
            if (confirmFlag)
            {
                if (validateData())
                {
                    loginData();
                }
                else
                {
                    return;
                }
            }
                
        }); 
        > ex) ==================================================================================
    */
var MsgBox = {
    Alert: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            $("#btn_confirm").hide();
            $("#btn_alert").show();
            $("#msg_popup #alert_ok").unbind();

            setModalDesign(msg, "alert");

            $("#msg_popup").modal('show');
            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { resolve(); }
                else { }
            });
        }).then(okhandler).catch(function () { });
    },

    Warning: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            $("#btn_confirm").hide();
            $("#btn_alert").show();
            $("#msg_popup #alert_ok").unbind();

            setModalDesign(msg, "warning");

            $("#msg_popup").modal('show');
            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { resolve(); }
                else { }
            });
        }).then(okhandler).catch(function () { });
    },

    Danger: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            $("#btn_confirm").hide();
            $("#btn_alert").show();
            $("#msg_popup #alert_ok").unbind();

            setModalDesign(msg, "danger");

            $("#msg_popup").modal('show');
            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { resolve(); }
                else { }
            });
        }).then(okhandler).catch(function () { });
    },

    Inform: function (msg, okhandler) {
        new Promise((resolve, reject) => {
            $("#btn_confirm").hide();
            $("#btn_alert").show();
            $("#msg_popup #alert_ok").unbind();

            setModalDesign(msg, "inform");

            $("#msg_popup").modal('show');
            $("#msg_popup #alert_ok").click(function () {
                $("#msg_popup").modal("hide");
            });

            $("#msg_popup").on("hidden.bs.modal", function (e) {
                e.stopPropagation();
                if (okhandler != null) { resolve(); }
                else { }
            });
        }).then(okhandler).catch(function () { });
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

/**
 * - ModalBox 
 * - 사용자 Custom Modal 호출
 * - /Controller명/Action명, Data 를 입력하여 특정 View 를 Modal Popup 으로 호출한단.
 */
var ModalBox = {
    Show: function (controllerActionUrl, requestData) {
        $.ajax({
            type: "POST",
            url: controllerActionUrl,
            data: requestData,
            success: function (responseData)
            {
                $("#inner_custom_popup").html(responseData);
                $("#custom_popup").modal({backdrop:'static', keyboard:false}); // 다른영역 클릭 시 안닫히게 하는 소스
                $("#custom_popup").modal("show"); 
            }

        });
    }

}

/**
    * - setModalDesign
    * @param {any} message - 팝업 내부에 작성할 메시지
    * @param {any} status  - 팝업 디자인을 제공하기 위한 기준 상태분류
*/
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

    // Text Fill
    $("#amhr_modal_title").html(modal_title);
    $("#amhr_modal_body_text").html(modal_message);

    // Design class Change
    $("#amhr_modal_header").attr("class", "modal-header " + bgColorDesign + " " + fontColorDesign);
    $("button[name='amhr_btn_ok']").attr("class", "btn " + btnBgColorDesign);
}

/**
 * - setErrCustormContents
 * @param {any} data - ajax Error Data
 * @returns - string message(error 관련 message 조합 후 반환)
 */
function setErrCustomContents(data)
{
    var msg = "";

    if (data != null)
    {
        if (data.status != null)
        {
            msg += "<div class='amhr_error_div'>";
            msg += "<p>";
            msg += "Error status : " + data.status;
            msg += "</p>";
        }

        if (data.statusText != null)
        {
            msg += "<p>";
            msg += "Error status : " + data.statusText;
            msg += "</p>";
        }

        if (data.responseText != null)
        {
            msg += "<p>";
            msg += "Error Detail : " + "</br>";
            msg += $($(data.responseText).find("table")[0]).prop("outerHTML");
            msg += "</p>";

            msg += "<p>";
            msg += $($(data.responseText).find("table")[1]).prop("outerHTML");
            msg += "</p>";
        }
        msg += "</div>";
    }
        

    return msg;
}

/**
 * - Nav_Bar_DropDownToggle
 * - 상단 로그인 이후 생기는 DropDown 클릭 시 호출되는 함수
 */
function Nav_Bar_DropDownToggle()
{
    var navBarDropDownClasses = $("#amhr-nav-bar-dropdown").attr('class').split(' ');
    var length = 0;
    if (navBarDropDownClasses != null && navBarDropDownClasses != undefined)
    {
        length = navBarDropDownClasses.length;
    }

    if (length > 0)
    {
        var flag = false;
        for (var i = 0; i < length; i++)
        {
            if (navBarDropDownClasses[i] == "show")
            {
                flag = true;
            }
        }

        switch (flag)
        {
            case false:
                $("#amhr-nav-bar-dropdown").addClass("show");
                break;
            case true:
                $("#amhr-nav-bar-dropdown").removeClass("show");
                break;
            default:
                break;
        }
    }
}

/**
 * - Nav_Bar_DropDownClose
 * - 상단 로그인 이후 생기는 DropDown 을 제외한 영역을 클릭했을 때 호출되는 함수
 */
function Nav_Bar_DropDownClose() {
    $("#amhr-nav-bar-dropdown").removeClass("show");
}

/**
 * - WarningGlobalMessage
 * - 전역으로 Warning 알림메시지 호출
 * @param {any} message - 안내메시지
 */
function WarningGlobalMessage(message) {
    MsgBox.Warning(message);
}

/**
 * - ChangeConstPageNumber
 * - 페이징에서 번호를 클릭할 때 호출되는 전역함수
 * @param {any} pageNumber - 페이지 번호
 */
function ChangeConstPageNumber(pageNumber) {
    $("#CONST_PAGE_NUMBER").val(pageNumber);
    $("#CONST_SEARCH_FORM").submit();
}

/**
 * - initEnterSubmitEvent
 * - 특정 페이지에서 사용되는 Input 요소에 Enter 입력 시 Form Submit 하는 이벤트
 */
function InitEnterSubmitEvent()
{
    $("input[type='text']").keypress(function (e) {
        if (e.keyCode == 13)
        {
            $("#CONST_SEARCH_FORM").submit();
        }
    });
}


/*
  RefreshCondition : 검색조건 초기화
*/
function RefreshCondition() {
    $("input[type='text']").val('')
}

/*
    RefreshPagenum : 페이지 번호 초기화
*/
function RefreshPagenum() {
    $("#CONST_PAGE_NUMBER").val("0");
}

/*
    SubmitForm : 폼 서브밋(검색)
*/
function SubmitForm(submitFlag) {
    var flag = submitFlag;
    if (flag == "SEARCH") {
        RefreshPagenum();
        $("#CONST_SEARCH_FORM").submit();
    }
    else (flag == "")
    {
        $("#CONST_SEARCH_FORM").submit();
    }
}
