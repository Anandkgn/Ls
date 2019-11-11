<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Common/YPMaster.Master" AutoEventWireup="true" CodeBehind="frmSection1Reg.aspx.cs" Inherits="LsWb.UI.Common.frmSection1Reg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../Home/CSS/CustomPopUp/jquery-1.7.2.min.js"></script>
    <script src="../../Home/CSS/CustomPopUp/jquery-ui-1.8.9.js"></script>
    <link href="../../Home/CSS/CustomPopUp/jquery-ui-1.8.9.css" rel="stylesheet" />

    <%-- <script src="http://code.jquery.com/jquery-latest.js"></script>--%>


    <style type="text/css">
        .loader {
            color: #ffffff;
            font-size: 20px;
            margin: 100px auto;
            width: 1em;
            height: 1em;
            border-radius: 50%;
            position: relative;
            text-indent: -9999em;
            -webkit-animation: load4 1.3s infinite linear;
            animation: load4 1.3s infinite linear;
            -webkit-transform: translateZ(0);
            -ms-transform: translateZ(0);
            transform: translateZ(0);
        }

        @-webkit-keyframes load4 {
            0%, 100% {
                box-shadow: 0 -3em 0 0.2em, 2em -2em 0 0em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 0;
            }

            12.5% {
                box-shadow: 0 -3em 0 0, 2em -2em 0 0.2em, 3em 0 0 0, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 -1em;
            }

            25% {
                box-shadow: 0 -3em 0 -0.5em, 2em -2em 0 0, 3em 0 0 0.2em, 2em 2em 0 0, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 -1em;
            }

            37.5% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0em 0 0, 2em 2em 0 0.2em, 0 3em 0 0em, -2em 2em 0 -1em, -3em 0em 0 -1em, -2em -2em 0 -1em;
            }

            50% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 0em, 0 3em 0 0.2em, -2em 2em 0 0, -3em 0em 0 -1em, -2em -2em 0 -1em;
            }

            62.5% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 0, -2em 2em 0 0.2em, -3em 0 0 0, -2em -2em 0 -1em;
            }

            75% {
                box-shadow: 0em -3em 0 -1em, 2em -2em 0 -1em, 3em 0em 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 0, -3em 0em 0 0.2em, -2em -2em 0 0;
            }

            87.5% {
                box-shadow: 0em -3em 0 0, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 0, -3em 0em 0 0, -2em -2em 0 0.2em;
            }
        }

        @keyframes load4 {
            0%, 100% {
                box-shadow: 0 -3em 0 0.2em, 2em -2em 0 0em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 0;
            }

            12.5% {
                box-shadow: 0 -3em 0 0, 2em -2em 0 0.2em, 3em 0 0 0, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 -1em;
            }

            25% {
                box-shadow: 0 -3em 0 -0.5em, 2em -2em 0 0, 3em 0 0 0.2em, 2em 2em 0 0, 0 3em 0 -1em, -2em 2em 0 -1em, -3em 0 0 -1em, -2em -2em 0 -1em;
            }

            37.5% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0em 0 0, 2em 2em 0 0.2em, 0 3em 0 0em, -2em 2em 0 -1em, -3em 0em 0 -1em, -2em -2em 0 -1em;
            }

            50% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 0em, 0 3em 0 0.2em, -2em 2em 0 0, -3em 0em 0 -1em, -2em -2em 0 -1em;
            }

            62.5% {
                box-shadow: 0 -3em 0 -1em, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 0, -2em 2em 0 0.2em, -3em 0 0 0, -2em -2em 0 -1em;
            }

            75% {
                box-shadow: 0em -3em 0 -1em, 2em -2em 0 -1em, 3em 0em 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 0, -3em 0em 0 0.2em, -2em -2em 0 0;
            }

            87.5% {
                box-shadow: 0em -3em 0 0, 2em -2em 0 -1em, 3em 0 0 -1em, 2em 2em 0 -1em, 0 3em 0 -1em, -2em 2em 0 0, -3em 0em 0 0, -2em -2em 0 0.2em;
            }
        }
    </style>
    <!-- For Progress bar -->

    <style type="text/css">
        .diolog-box {
            width: 300px;
            top: 382px;
        }

        .ui-dialog {
            display: block;
            z-index: 1002;
            outline: 0px;
            position: fixed;
            height: auto;
            width: 300px;
            top: 25% !important;
            left: 410px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.1;
            filter: alpha(opacity=1);
            -moz-opacity: 0.5;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            /*font-family: Arial;
            font-size: 10pt;*/
            border: 0px;
            width: 100%;
            height: 100%;
            display: none;
            position: fixed;
            /*background-color: rgba(105, 128, 127, 0.50);*/
            background-color: rgba(105, 128, 127, 0.50);
            z-index: 999;
            color: black;
        }

        .imgCenter {
            padding-top: 220px;
        }
    </style>

    <style type="text/css">
        .star {
            color: Red;
        }
    </style>

    <!-- For Calender control-->

    <style type="text/css" runat="server">
        /*/* Custom theme  for Calender  control */
        .cal_Theme1 .ajax__calendar_container {
            width: 220%;
            background-color: #337ab7;
            border: solid 1px #337ab7;
            font-family: Arial;
        }

        .star {
            color: Red;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #fff;
            margin-bottom: 4px;
            text-align: center;
            vertical-align: central;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            width: 100%;
            background-color: #fff;
            border: solid 1px #337ab7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
            width: 33px;
        }

        .cal_Theme1 .ajax__calendar_day {
            width: 33px;
            color: #337ab7;
            text-align: center;
            vertical-align: central;
            font-weight: bold;
            font-size: 12px;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #fff;
            font-weight: bold;
            background-color: #337ab7;
            font-size: 14px;
            font-family: Arial;
        }

        .cal_Theme1 .ajax__calendar_leave .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_leave .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_leave .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #337ab7;
            font-weight: bold;
            background-color: #fff;
            font-family: Arial;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
            color: #fff;
            font-size: 11px;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }

        .cal_Theme1 .ajax__calendar_invalid {
            color: red;
            text-decoration: line-through;
            cursor: not-allowed;
            background-color: lightpink;
        }
    </style>


    <script type="text/javascript">

        function Focus() {

            var id = document.getElementById('<%= hffocus.ClientID %>').value;
            //id = '#' + id;
            //alert(id);
            //var ctrlName = $(id).val();

            // alert(ctrlName);



            var ctrl = 'ContentPlaceHolder1_' + id;


            $('#' + ctrl).focus();


        }
    </script>
    <%-------------------------Start hide progress bar--------------------------------%>

    <script type="text/javascript">
        function hideProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.hide();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        //var width = $(window).width();
        //var height = $(window).height();

        // alert(width); alert(height);

    </script>

    <%-------------------------end hide progress bar--------------------------------%>

    <%-------------------------Start show progress bar--------------------------------%>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function showLoading() {
            // alert('d');
            ShowProgress();
        }

        function stopLoading() {

            document.getElementById('<%= hfkeypress.ClientID %>').value = "0";
            Focus();
            hideProgress();
        }

        function stopLoadingNew() {

            document.getElementById('<%= hfkeypress.ClientID %>').value = "0";

            hideProgress();
        }

        $('form').live("submit", function () {
            //  ShowProgress();
        });
    </script>

    <%-------------------------end show progress bar--------------------------------%>


    <%-------------------------Start Modal Pop Up--------------------------------%>

    <script type="text/javascript">

        function ShowPopup(message, ctrlName) {
            $(function () {
                stopLoading();
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    buttons: {
                        OK: function () {
                            $(this).dialog('close');
                            $('#ContentPlaceHolder1_' + ctrlName).focus();

                        }
                    },
                    modal: true,
                    draggable: false,
                    resizable: false
                });
            });
        };

        function ShowPopupContent(message, ctrlName) {
            $(function () {

                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    buttons: {
                        OK: function () {
                            $(this).dialog('close');
                            //$('#' + ctrlName).val('');

                            $('#' + ctrlName).focus();

                        }
                    },
                    modal: true,
                    draggable: false,
                    resizable: false
                });
            });
        };

        function ShowPopupContentServer() {
            $(function () {

                stopLoading();
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    buttons: {
                        OK: function () {
                            $(this).dialog('close');
                            //$('#' + ctrlName).val('');

                            // $('#' + ctrlName).focus();

                        }
                    },
                    modal: true,
                    draggable: false,
                    resizable: false
                });
            });
        };

        function ShowPopupSuccess(message, ctrlName) {
            $(function () {

                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    buttons: {
                        OK: function () {
                            $(this).dialog('close');

                            window.location = "frmHomePage.aspx";
                        }
                    },
                    modal: true,
                    draggable: false,
                    resizable: false
                });
            });
        };
        function ShowPopupSuccessAndRedirect(message, ctrlName, formname) {
            $(function () {
                stopLoading();
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    //buttons: {
                    //    OK: function () {
                    //        $(this).dialog('close');

                    //        window.location = formname;
                    //    }
                    //},

                    modal: true,
                    draggable: false,
                    resizable: false
                });

                setTimeout(function () { window.location = formname; }, 1000);

            });
        };




        function Redirect(formname) {

            window.location = formname;

        };



    </script>


    <script language="javascript" type="text/javascript">

        function Change(obj, evt) {


            if (evt.type == "focus") {
                obj.className = "onfocus";
            }
            else if (evt.type == "blur")
                obj.className = "onblur";
        }

        // for CharKey

        function isCharKey(obj, evt) {
            var ctrl = obj.id;
            var charCode = evt.which;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode < 09) || charCode == 32 || charCode == 13) {
                return true;
            }
            else {
                $('#' + ctrl).val('');
                ShowPopupContent('Please enter alphabets only', ctrl);
                return false;
            }


        }

        // for NumberKey
        function isNumberKey(obj, evt) {

            var ctrl = obj.id;
            var charCode = evt.which;

            if (charCode >= 48 && charCode <= 57 || charCode == 8 || charCode == 0 || charCode == 9 || charCode == 13) {
                return true;
            }
            else {
                //$('#' + ctrl).val('');
                ShowPopupContent("Please enter only numerical value", ctrl);
                return false;
            }

            return true;
        }

        // for AlphaNumeric
        function txtAlphanumeric(obj, evt) {
            var ctrl = obj.id;
            var charCode = evt.which;

            if ((charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode < 09) || charCode == 32 || charCode == 13) {
                return true;
            }
            else {
                $('#' + ctrl).val('');
                ShowPopupContent('Please enter alpha-numeric only', ctrl);
                return false;
            }


        }

        // for Mobile number

        function validateMobileNo(obj, evt) {
            alert(obj);
            var ctrl = obj.id;
            var mob = /^[7-9]{1}[0-9]{9}$/;
            var MobileNo = $('#' + ctrl).val();

            //   var txtMobile = document.getElementById(MobileNo);
            if (mob.test(obj.value) == false) {
                $('#' + ctrl).val('');
                ShowPopupContent('Please enter valid mobile number', ctrl);
                return false;
            }
            return true;
        }



        // for Landline number
        function validatePhoneno(obj, evt) {

            var ctrl = obj.id;
            PreventFirstCharZero(ctrl);

            var phone = $('#' + ctrl).val();

            if (phone.length < 6 || phone.length > 13) {
                $('#' + ctrl).val('');
                ShowPopupContent("Please enter valid phone number", ctrl);
                return false;
            }

            return true;

        }

        // for Email
        function validateEmailId(obj, evt) {

            var ctrl = obj.id;

            var Email = $('#' + ctrl).val();


            //var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            var mailformat = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (Email.match(mailformat)) {
                return true;
            }
            else {
                $('#' + ctrl).val('');
                ShowPopupContent("Invalid Email ID entered! Please enter valid Email ID", ctrl);
                return false;
            }
        }

        // for Pincode

        function validatePincode(obj, evt) {

            var ctrl = obj.id;
            var pincode = $('#' + ctrl).val();

            if (pincode.length < 6) {
                $('#' + ctrl).val('');
                ShowPopupContent("Please enter 6 digits pincode number", ctrl);
                return false;

            }

            return true;

        }

        // for Age

        function validateAge(obj, evt) {

            var ctrl = obj.id;
            var Age = $('#' + ctrl).val();

            if (Age < 1 || Age > 125) {
                $('#' + ctrl).val('');
                ShowPopupContent("Please enter Age Between 1 to 125", ctrl);
                return false;

            }

            return false;
        }




        //for min VAlue check
        function CheckMinVal(obj, evt, LblText, _MinValue) {

            var ctrl = obj.id;
            var charCode = evt.which;

            var MLen = 0;
            if (_MinValue != '' && _MinValue != 'NULL' && _MinValue != null) {
                Mval = parseInt(_MinValue);


                var Val = $('#' + ctrl).val();

                if (Val != '') {


                    if (parseInt(Val) < Mval) {
                        $('#' + ctrl).val('');
                        ShowPopupContent(LblText + " should not be lesser than " + _MinValue, ctrl);

                        return false;

                    }
                }
            }

            return true;
        }
        //for MAX VAlue check
        function CheckMaxVal(obj, evt, LblText, _MaxValue) {

            var ctrl = obj.id;
            var charCode = evt.which;

            var MLen = 0;
            if (_MaxValue != '' && _MaxValue != 'NULL' && _MaxValue != null) {
                Mval = parseInt(_MaxValue);


                var Val = $('#' + ctrl).val();

                if (Val != '') {


                    if (parseInt(Val) > Mval) {
                        $('#' + ctrl).val('');
                        ShowPopupContent(LblText + " should not be greater than " + _MaxValue, ctrl);

                        return false;

                    }
                }
            }

            return true;
        }




        //for min length check
        function ChkLength(obj, evt, LblText, MinLength) {

            var ctrl = obj.id;
            var charCode = evt.which;

            var MLen = 0;
            if (MinLength != '' && MinLength != 'NULL' && MinLength != null) {
                MLen = parseInt(MinLength);


                var Val = $('#' + ctrl).val();

                if (Val != '') {


                    if (Val.length < MLen) {
                        $('#' + ctrl).val('');
                        ShowPopupContent(LblText + " should be " + MinLength + " digit(s)", ctrl);

                        return false;

                    }
                }
            }

            return true;
        }


        //for min VAlue check
        function CheckVal(obj, evt, LblText, _MinValue) {

            var ctrl = obj.id;
            var charCode = evt.which;

            var MLen = 0;
            if (_MinValue != '' && _MinValue != 'NULL' && _MinValue != null) {
                Mval = parseInt(_MinValue);


                var Val = $('#' + ctrl).val();

                if (Val != '') {


                    if (parseInt(Val) > Mval) {
                        $('#' + ctrl).val('');
                        ShowPopupContent(LblText + " should not be greater than" + _MinValue, ctrl);

                        return false;

                    }
                }
            }

            return true;
        }

        //keypress and onchange event 

        function OnKeyPress(obj, evt, _retType) {

            var RetVal = true;

            if (_retType == "Text") {

                RetVal = isCharKey(obj, evt);

                if (!RetVal) return false;
            }
            else if (_retType == "Number" || _retType == "Mobile" || _retType == "Phone" || _retType == "Age" || _retType == "Pincode") {

                RetVal = isNumberKey(obj, evt);


                if (!RetVal) return false;
            }

            else if (_retType == "Alphanumeric") {

                RetVal = txtAlphanumeric(obj, evt);


                if (!RetVal) return false;
            }

        }

        function OnChange(obj, evt, _retType, _minLength, _MinValue, LblText, _IsDuplicateChk, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate, CtrlType) {

            var RetVal = true;

            if (_retType == "Email") {

                RetVal = validateEmailId(obj, evt);

                if (!RetVal) return false;
            }
            else if (_retType == "Mobile") {

                RetVal = validateMobileNo(obj, evt);

                if (!RetVal) return false;
            }
            else if (_retType == "Phone") {

                RetVal = validatePhoneno(obj, evt);

                if (!RetVal) return false;
            }
            else if (_retType == "Age") {

                RetVal = validateAge(obj, evt);

                if (!RetVal) return false;
            }
            else if (_retType == "Pincode") {

                RetVal = validatePincode(obj, evt);

                if (!RetVal) return false;
            }

            RetVal = ChkLength(obj, evt, LblText, _minLength);


            if (!RetVal) return false;

            RetVal = CheckMinVal(obj, evt, LblText, _MinValue);
            if (!RetVal) return false;

            RetVal = CheckMaxVal(obj, evt, LblText, _MaxValue);
            if (!RetVal) return false;


            if (_IsDuplicateChk == "True" || _IsDuplicateChk == "1") {


                RetVal = DuplicateChk(obj, evt, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate, CtrlType);



            }
            return true;

        }


        //dropdown onchange
        function DdlOnChange(obj, evt, _IsCrossValidation, _ColToEnableOrDisable, _Value, _ColType, _Operation, _autoPostback, _Qid, CtrlType, _IsDuplicateChk, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate) {

            var RetVal = true;




            validateDropdown(obj, evt, _IsCrossValidation, _ColToEnableOrDisable, _Value, _ColType, _Operation, _autoPostback, _Qid);



            if (_IsDuplicateChk == "True" || _IsDuplicateChk == "1") {


                RetVal = DuplicateChk(obj, evt, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate, CtrlType);


            }

            return true;
        }

        //checkbox onclick
        function ClbOnChange(obj, evt, _IsCrossValidation, _ColToEnableOrDisable, _Value, _ColType, _Operation, _autoPostback, _Qid, CtrlType) {

            var RetVal = true;



            validatechkbox(obj, evt, _IsCrossValidation, _ColToEnableOrDisable, _Value, _ColType, _Operation, _autoPostback, _Qid);

            if (_IsDuplicateChk == "True" || _IsDuplicateChk == "1") {


                RetVal = DuplicateChk(obj, evt, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate, CtrlType);


            }

            return true;


        }

        //date change
        function DateOnChange(obj, evt, _IsCrossValidation, _ColToEnableOrDisable, _Value, _ColType, _Operation, _autoPostback, _Qid, CtrlType, _IsDuplicateChk, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate) {

            var RetVal = true;


            RetVal = isDate(obj, evt);



            if (_IsDuplicateChk == "True" || _IsDuplicateChk == "1") {


                RetVal = DuplicateChk(obj, evt, _QryToChkDuplicate, _ColToChkForDuplicate, _DbColToChkForDuplicate, CtrlType);


            }

            return true;



        }
    </script>

    <%-------------------------end Common Validation--------------------------------%>

    <script language="javascript" type="text/javascript">

        function PreventFirstCharZero(ctrl) {
            var slumid = $('#' + ctrl).val();
            if (slumid == "") {
                slumid = '0';
            }
            var total = parseInt(slumid)

            if (total == 0) {
                $('#' + ctrl).val('');
                ShowPopupContent('Please enter valid Number.It should not be zero', ctrl);
                return false;
            }
        }

    </script>


    <%-------------------------Start Date Validation--------------------------------%>

    <script language="javascript" type="text/javascript">

        function isDate(obj, evt) {

            var ctrl = obj.id;


            return true;


        }

        function validateButton(obj, evt, Val, Message, type, ctrlName, PreBtnId, Id) {

            var ctrl = obj.id;

            var ctrl = "";

            //----------------------Checking control type-----------------------------------------------------///
            if (type == "cal" || type == "Time") {

                ctrl = "ctl00_ContentPlaceHolder1_" + ctrlName;
            }
            else {
                ctrl = 'ContentPlaceHolder1_' + ctrlName;
            }

            if (Id == 0) {
                return true;
            } else if (Id > 0) {

                Message = 'Please fill Section ' + Id + ' first';

                var color = $('#ContentPlaceHolder1_' + PreBtnId).css("background-color");

                if (color == "rgb(0, 128, 0)") {
                    return true;

                } else if (color == "rgb(255, 165, 0)") {


                    ShowPopupContent(Message, ctrl); return false;
                }


            }

            //if (Val == "0" && Message != "") {

            //    ShowPopupContent(Message, ctrl); return false;

            //}

            return true;


        }

    </script>


    <%-------------------------end Date Validation--------------------------------%>

    <%-------------------------Start Dropdown Validation--------------------------------%>

    <script language="javascript" type="text/javascript">

        function validateDropdown(obj, evt, _IsParent, _ColNames, _ColValue, _ColType, _Operation, _IsPostback, _Qid) {
            var ctrl = obj.id;

            var value = $('#' + ctrl).val();

            if (_IsPostback == "True") {
                $('#' + ctrl).attr('disabled', true);
                document.getElementById('<%= hfkeypress.ClientID %>').value = "1";

                showLoading();
            }



            var oTable = JSON.parse($("#ContentPlaceHolder1_hdnControlChild").val());

            for (var i = 0; i < oTable.length; i++) {


                var fldQnId = oTable[i]["fldQnId"];
                var fldFormId = oTable[i]["fldFormId"];
                var fldIsCrossValidation = oTable[i]["fldIsCrossValidation"];
                var fldOperation = oTable[i]["fldOperation"];
                var fldValue = oTable[i]["fldValue"];
                var fldColType = oTable[i]["fldColType"];
                var fldColToEnableOrDisable = oTable[i]["fldColToEnableOrDisable"];
                var fldIsNotEmptyValue = oTable[i]["fldIsNotEmptyValue"];

                //alert(fldValue);

                var IsChkValid = false;

                if (_Qid == fldQnId && _IsParent.toString().toUpperCase() == fldIsCrossValidation.toString().toUpperCase()) {//&& fldValue == value


                    _ColNames = fldColToEnableOrDisable;
                    _ColValue = fldValue;
                    _ColType = fldColType;
                    _Operation = fldOperation;

                    IsChkValid = true;
                    //break;
                }

                if (IsChkValid) {



                    var result = ((_ColNames.indexOf(",") == -1) ? false : true);





                    if (result) {


                        var _ColNamesArray = new Array();
                        _ColNamesArray = _ColNames.split(",");

                        var _ColTypeArray = new Array();
                        _ColTypeArray = _ColType.split(",");

                        var cnt = 0; var IsValid = false;
                        for (cnt = 0; cnt < _ColNamesArray.length; cnt++) {

                            var ctrldep = "";
                            //----------------------Checking control type-----------------------------------------------------///
                            if (_ColTypeArray[cnt] == "cal" || _ColTypeArray[cnt] == "Time") {

                                ctrldep = "ctl00_ContentPlaceHolder1_" + _ColNamesArray[cnt];
                            }
                            else {
                                ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray[cnt];
                            }
                            //----------------------------------------------------////////////////////////

                            var valuedep = $('#' + ctrldep).val();

                            //alert(fldIsNotEmptyValue);

                            var IsChkNext = false;
                            if (fldIsNotEmptyValue == true) {
                                IsChkNext = true;

                            }

                            //alert(IsChkNext);

                            if (_ColValue == value || (IsChkNext == true && _ColValue == "" && value != "--Select--")) {

                                if (_ColTypeArray[cnt] == "3") {
                                    $('#' + ctrldep).val('--Select--');
                                }
                                else if (_ColTypeArray[cnt] == "4") {

                                }
                                else {
                                    $('#' + ctrldep).val('');
                                }

                                if (_Operation == "Disable") {



                                    $('#' + ctrldep).attr('disabled', true);

                                    if (_ColTypeArray[cnt] == "4") {
                                        $('#' + ctrldep + ' input').prop('checked', false);
                                        $('#' + ctrldep + ' input').prop('disabled', true);
                                    }

                                }
                                else if (_Operation == "Enable") {


                                    $('#' + ctrldep).attr('disabled', false);
                                    if (_ColTypeArray[cnt] == "4") {
                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                        $('#' + ctrldep + ' input').prop('disabled', false);
                                    }
                                }
                            }
                            else if (_ColValue != value || (IsChkNext == true && _ColValue == "" && value == "--Select--")) {

                                if (_ColTypeArray[cnt] == "3") {
                                    $('#' + ctrldep).val('--Select--');
                                }
                                else if (_ColTypeArray[cnt] == "4") {

                                }
                                else {
                                    $('#' + ctrldep).val('');
                                }

                                if (_Operation == "Disable") {


                                    $('#' + ctrldep).attr('disabled', false);
                                    if (_ColTypeArray[cnt] == "4") {
                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                        $('#' + ctrldep + ' input').prop('disabled', false);
                                    }
                                }
                                else if (_Operation == "Enable") {


                                    $('#' + ctrldep).attr('disabled', true);
                                    if (_ColTypeArray[cnt] == "4") {
                                        $('#' + ctrldep + ' input').prop('checked', false);
                                        $('#' + ctrldep + ' input').prop('disabled', true);
                                    }
                                }
                            }


                        }


                    }
                    else if (!result) {

                        var _ColNamesArray = _ColNames;

                        var _ColTypeArray = _ColType;

                        var ctrldep = "";
                        //----------------------Checking control type-----------------------------------------------------///
                        if (_ColTypeArray == "cal" || _ColTypeArray == "Time") {

                            ctrldep = "ctl00_ContentPlaceHolder1_" + _ColNamesArray;
                        }
                        else {
                            ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray;
                        }
                        //----------------------------------------------------////////////////////////

                        var valuedep = $('#' + ctrldep).val();

                        //alert(fldIsNotEmptyValue);

                        var IsChkNext = false;
                        if (fldIsNotEmptyValue == true) {
                            IsChkNext = true;

                        }

                        //alert(IsChkNext);

                        if (_ColValue == value || (IsChkNext == true && _ColValue == "" && value != "--Select--")) {

                            if (_ColTypeArray == "3") {

                                $('#' + ctrldep).val('--Select--');
                            }
                            else if (_ColTypeArray == "4") {


                            }
                            else {

                                $('#' + ctrldep).val('');
                            }

                            if (_Operation == "Disable") {

                                //$('#' + ctrldep).val('');
                                $('#' + ctrldep).attr('disabled', true);
                                if (_ColTypeArray == "4") {
                                    $('#' + ctrldep + ' input').prop('checked', false);
                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                }
                            }
                            else if (_Operation == "Enable") {
                                // $('#' + ctrldep).val('');
                                $('#' + ctrldep).attr('disabled', false);
                                if (_ColTypeArray == "4") {
                                    // $('#' + ctrldep + ' input').prop('checked', false);
                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                }

                            }
                        }
                        else if (_ColValue != value || (IsChkNext == true && _ColValue == "" && value == "--Select--")) {

                            if (_ColTypeArray == "3") {

                                $('#' + ctrldep).val('--Select--');
                            }
                            else if (_ColTypeArray == "4") {


                            }
                            else {

                                $('#' + ctrldep).val('');
                            }

                            if (_Operation == "Disable") {

                                //$('#' + ctrldep).val('');
                                $('#' + ctrldep).attr('disabled', false);
                                if (_ColTypeArray == "4") {
                                    // $('#' + ctrldep + ' input').prop('checked', false);
                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                }

                            }
                            else if (_Operation == "Enable") {

                                // $('#' + ctrldep).val('');
                                $('#' + ctrldep).attr('disabled', true);
                                if (_ColTypeArray == "4") {
                                    $('#' + ctrldep + ' input').prop('checked', false);
                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                }
                            }
                        }

                    }
                }
            }
            return true;
        }

    </script>


    <%-------------------------end Dropdown Validation--------------------------------%>


    <%-------------------------Start button save Validation--------------------------------%>

    <script language="javascript" type="text/javascript">
        function Validation(DivId) {



            var oTable = JSON.parse($("#ContentPlaceHolder1_hdnControl").val());

            var EnteredFieldCount = 0; var CtrlFocus = '';

            var IsGridAvailable = $("#ContentPlaceHolder1_hdnFrmCtrlDetails").val();

            var hfGridDet = $("#ContentPlaceHolder1_hfGridDetails").val();



            for (var i = 0; i < oTable.length; i++) {

                var Name = oTable[i]["fldQnEngText"];
                var type = oTable[i]["fldQnWidgetId"];
                var ctrlName = oTable[i]["fldQnWidgetName"];
                var Mandatory = oTable[i]["fldQnMandatory"];
                var IsDependent = oTable[i]["fldIsDependent"];
                var DependentColNames = oTable[i]["fldDependentColNames"];
                var DependentColValues = oTable[i]["fldDependentColValues"];
                var DependentColType = oTable[i]["fldDependentColType"];
                var _DivId = oTable[i]["fldDivID"];
                var _IsGridData = oTable[i]["fldIsGridData"];
                var _IsAutoBindOrInputQn = oTable[i]["fldIsAutoBindOrInputQn"];

                if (DivId == _DivId || DivId == 0) {



                    var ctrl = "";
                    //----------------------Checking control type-----------------------------------------------------///
                    if (type == "cal" || type == "Time") {

                        ctrl = "ctl00_ContentPlaceHolder1_" + ctrlName;
                    }
                    else {
                        ctrl = 'ContentPlaceHolder1_' + ctrlName;
                    }
                    //----------------------------------------------------////////////////////////


                    CtrlFocus = ctrl;
                    var value = $('#' + ctrl).val();

                    if (type == "4") {

                        var checkboxValues = [];
                        $('#' + ctrl + ' input:checkbox:checked').map(function () {
                            checkboxValues.push($(this).val());
                        });

                        value = checkboxValues;
                    }
                    if (type == "9") {

                        value = $("input:radio[name='ctl00$ContentPlaceHolder1$rbtnTrainingDetails']:checked").val();
                        alert(value);
                    }
                    else if (type == "1") {
                        value = value.trim();
                    }

                    //alert(Mandatory);
                    //alert(value);
                    //alert($('#' + ctrl).prop("disabled"));


                    // alert(value);

                    if ((Mandatory == "1" || Mandatory == "true") && ($('#' + ctrl).prop("disabled") == false || type == "9") && (value == "" || value == "--Select--" || value == null || value == undefined || value == "undefined")) {


                        if (type == "1") {
                            // alert(ctrlName);
                            if (ctrlName == "ddlAlreadyTrained") {
                                ShowPopupContent(' Please select atleast one trained officer from trained officer details', '');
                            }
                            else {

                                ShowPopupContent(' Please enter  "' + Name + '"', ctrl);
                            }

                        }
                        else {

                            ShowPopupContent(' Please select  "' + Name + '"', ctrl);
                        }
                        return false;
                    }


                    if (DivId == 0 && (_IsGridData == "false" || _IsGridData == false)) {


                        if ((Mandatory == "1" || Mandatory == "true") && $('#' + ctrl).prop("disabled") == false && (value == "" || value == "--Select--" || value == null)) {


                            if (type == "1") {

                                if (ctrlName == "ddlAlreadyTrained") {
                                    ShowPopupContent(' Please select atleast one trained officer from trained officer details', '');
                                }
                                else {

                                    ShowPopupContent(' Please enter  "' + Name + '"', ctrl);
                                }

                            }
                            else {

                                ShowPopupContent(' Please select  "' + Name + '"', ctrl);
                            }
                            return false;
                        }
                    }

                    //if (type == '8') {

                    //    IsGridAvailable = 1;
                    //}

                    if (value != "" && value != "--Select--" && value != null && type != '7' && type != '8' && (_IsGridData != "1" && _IsGridData != "true") && _IsAutoBindOrInputQn != "1") {

                        EnteredFieldCount = EnteredFieldCount + 1;
                    }



                    if (IsDependent == "1" && $('#' + ctrl).prop("disabled") == false && (value == "" || value == "--Select--" || value == null) && Mandatory == "0") {


                        var result = ((DependentColNames.indexOf(",") == -1) ? false : true);

                        if (result) {



                            var colnames = new Array();
                            if (DependentColNames != "") {
                                colnames = DependentColNames.split(",");
                            }

                            var colvalues = new Array();
                            if (DependentColValues != "") {
                                colvalues = DependentColValues.split(",");
                            }

                            var coltype = new Array();
                            if (DependentColType != "") {
                                coltype = DependentColType.split(",");
                            }

                            var cnt = 0; var IsChk = false;
                            for (cnt = 0; cnt < colnames.length; cnt++) {

                                var ctrldep = "";
                                //----------------------Checking control type-----------------------------------------------------///
                                if (coltype[cnt] == "cal" || coltype[cnt] == "Time") {

                                    ctrldep = "ctl00_ContentPlaceHolder1_" + colnames[cnt];
                                }
                                else {
                                    ctrldep = 'ContentPlaceHolder1_' + colnames[cnt];
                                }
                                //----------------------------------------------------////////////////////////

                                var valuedep = $('#' + ctrldep).val();

                                if (valuedep == colvalues[cnt]) {
                                    IsChk = true;
                                } else {
                                    IsChk = false; break;
                                }

                            }

                            if (IsChk == true) {

                                if (type == "1") {

                                    ShowPopupContent(' Please enter the ' + Name, ctrl);

                                }
                                else {

                                    ShowPopupContent(' Please select the ' + Name, ctrl);
                                }
                                return false;
                            }
                        }
                        else if (!result) {


                            var colnames = DependentColNames;

                            var colvalues = DependentColValues;

                            var coltype = DependentColType;


                            var IsChk = false;

                            var ctrldep = "";
                            //----------------------Checking control type-----------------------------------------------------///
                            if (coltype == "cal" || coltype == "Time") {

                                ctrldep = "ctl00_ContentPlaceHolder1_" + colnames;
                            }
                            else {
                                ctrldep = 'ContentPlaceHolder1_' + colnames;
                            }
                            //----------------------------------------------------////////////////////////

                            var valuedep = $('#' + ctrldep).val();

                            if (valuedep == colvalues) {
                                IsChk = true;
                            } else {
                                IsChk = false; break;
                            }



                            if (IsChk == true) {

                                if (type == "1") {

                                    ShowPopupContent(' Please enter the ' + Name, ctrl);

                                }
                                else {

                                    ShowPopupContent(' Please select the ' + Name, ctrl);
                                }
                                return false;
                            }


                        }

                    }
                }

            }


            var _GridDataCount = 0; var _GridActualLen = 0;

            if (hfGridDet != '') {
                var result = ((hfGridDet.indexOf("_") == -1) ? false : true);


                if (result) {

                    var _GridArr = new Array();
                    _GridArr = hfGridDet.split("_");



                    for (var _GridCount = 0; _GridCount < _GridArr.length; _GridCount++) {

                        if (_GridArr[_GridCount] != '') {

                            _GridActualLen++;
                            var _GridVal = _GridArr[_GridCount];
                             var res = ((_GridVal.indexOf("&") == -1) ? false : true);
                            if (res) {

                                var _GArr = new Array();
                                _GArr = _GridVal.split("&");


                                for (var _GCount = 0; _GCount < _GArr.length; _GCount++) {

                                    if (_GCount > 0) {

                                        if (parseInt(_GArr[_GCount]) > 0) {

                                            _GridDataCount++;
                                        }


                                    }

                                }

                            }
                        }


                    }


                    //alert(_GridDataCount);
                    //alert(_GridActualLen);

                }

            }

            //alert(EnteredFieldCount);
            //alert(_GridDataCount);
            //alert(_GridActualLen);
            //alert(EnteredFieldCount);

            if (EnteredFieldCount == 0) {


                if (DivId == 0) {

                    if (_GridDataCount != _GridActualLen && _GridDataCount == 0 && _GridActualLen != 0) {

                        ShowPopupContent(' Atleast make one entry in grid to save partially ', CtrlFocus); return false;
                    }

                    else if (_GridDataCount == _GridActualLen && _GridDataCount != 0 && _GridActualLen != 0) {

                        return true;
                    }

                    else //if (_GridDataCount == 0 && _GridActualLen == 0) 
                    {

                        ShowPopupContent(' Atleast make one entry to save partially ', CtrlFocus); return false;
                    }


                }
            }


            // window.onbeforeunload = DisableButton();

            var retval = myFunction();
            if (!retval) return false;
            document.getElementById('<%= hfkeypress.ClientID %>').value = "1";


            ShowProgress();

        }

    </script>
    <%-------------------------end button save Validation--------------------------------%>

    <script type="text/javascript">

        function DuplicateChk(obj, evt, Qry, ColToChkForDuplicate, QnDbColName, ColType) {

            var ctrl = obj.id;

            var value = $('#' + ctrl).val();

            value = value.trim();

            var result = ((ColToChkForDuplicate.indexOf("$") == -1) ? false : true);

            var IsValid = false; var SqlQry = Qry;


            if (result) {

                var _ColNamesArray = new Array();
                _ColNamesArray = ColToChkForDuplicate.split("$");

                var _DbColNamesArray = new Array();
                _DbColNamesArray = QnDbColName.split("$");

                var cnt = 0;



                for (cnt = 0; cnt < _ColNamesArray.length; cnt++) {

                    var ctrldep = "";

                    ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray[cnt];

                    var valuedep = $('#' + ctrldep).val();


                    if (valuedep != "") {

                        IsValid = true; SqlQry = SqlQry + ' and ' + _DbColNamesArray[cnt] + '= ' + "&quot;" + valuedep + "&quot;";

                    }
                    else if (valuedep == "") {

                        IsValid = false; SqlQry = Qry;
                        break;
                    }

                }

                if (IsValid == true) {
                    Qry = SqlQry;
                }


            }
            else if (!result) {

                var _ColNamesArray = ColToChkForDuplicate;

                var SqlQry = Qry;


                var ctrldep = "";

                ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray;



                var valuedep = $('#' + ctrldep).val();

                if (valuedep != "") {


                    IsValid = true; SqlQry = SqlQry + ' and ' + QnDbColName + '= ' + "&quot;" + valuedep + "&quot;";

                }
                else if (valuedep == "") {

                    IsValid = false; SqlQry = Qry;

                }



                if (IsValid == true) {
                    Qry = SqlQry;
                }

            }

            //alert(Qry);
            //alert(SqlQry);



            if (IsValid == true) {

                if (value != "") {



                    document.getElementById('<%= hfkeypress.ClientID %>').value = "1";

                    showLoading();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: "{ Data: '" + value + "',Qry: '" + Qry + "'}",
                        url: "frmHomePage.aspx/ChkDuplicate",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == "0") {

                                stopLoadingNew();

                                //$('#' + ctrl).focus();

                                return true;
                            }
                            else if (data.d != "0") {

                                ShowPopupContent(' Record Already Exists ', ctrl);

                                stopLoadingNew();


                                if (ColType == 3) {
                                    $('#' + ctrl).val('--Select--');
                                }
                                else {
                                    $('#' + ctrl).val('');
                                }

                                //$('#' + ctrl).focus();

                                return false;

                            }
                        }
                    });
                }
            }

            return true;
        }

    </script>

    <script type="text/javascript">



        document.onkeydown = function myKeyFunction() {

            var retval = myFunction();


            if (!retval) return false;

        }


        function myFunction() {

            var id = document.getElementById('<%= hfkeypress.ClientID %>').id;
            id = '#' + id;

            var hftext = $(id).val();
            //alert(hftext);
            if (hftext == '1') {
                return false;
            }
            else if (hftext == '0') {
                return true;
            }

        }
    </script>


    <script language="javascript" type="text/javascript">

        function validatechkbox(obj, evt, _IsParent, _ColNames, _ColValue, _ColType, _Operation, _IsPostback, _Qid) {

            var ctrl = obj.id;

            var count = 0;
            var selectedItems = "";
            $('#' + ctrl + ' input:checkbox:checked').each(function () {
                selectedItems = $(this).next().html();
                count = count + 1;
                // alert(selectedItems);
            });


            //alert($('#' + ctrl + ' input:checkbox:checked').serialize());


            var checkboxValues = [];
            $('#' + ctrl + ' input:checkbox:checked').map(function () {
                checkboxValues.push($(this).val());
            });

            //alert(checkboxValues);



            if (_IsPostback == "True") {

                $('#' + ctrl).attr('disabled', true);

                document.getElementById('<%= hfkeypress.ClientID %>').value = "1";

                showLoading();
            }

            var oTable = JSON.parse($("#ContentPlaceHolder1_hdnControlChild").val());

            for (var i = 0; i < oTable.length; i++) {


                var fldQnId = oTable[i]["fldQnId"];
                var fldFormId = oTable[i]["fldFormId"];
                var fldIsCrossValidation = oTable[i]["fldIsCrossValidation"];
                var fldOperation = oTable[i]["fldOperation"];
                var fldValue = oTable[i]["fldValue"];
                var fldColType = oTable[i]["fldColType"];
                var fldColToEnableOrDisable = oTable[i]["fldColToEnableOrDisable"];




                if (_Qid == fldQnId && _IsParent.toString().toUpperCase() == fldIsCrossValidation.toString().toUpperCase()) {


                    _ColNames = fldColToEnableOrDisable;
                    _ColValue = fldValue;
                    _ColType = fldColType;
                    _Operation = fldOperation;

                    break;
                }

            }




            var result = ((_ColNames.indexOf(",") == -1) ? false : true);

            if (result) {


                var _ColNamesArray = new Array();
                _ColNamesArray = _ColNames.split(",");

                var _ColTypeArray = new Array();
                _ColTypeArray = _ColType.split(",");

                var cnt = 0; var IsValid = false;
                for (cnt = 0; cnt < _ColNamesArray.length; cnt++) {


                    var ctrldep = "";
                    //----------------------Checking control type-----------------------------------------------------///
                    if (_ColTypeArray[cnt] == "cal" || _ColTypeArray[cnt] == "Time") {

                        ctrldep = "ctl00_ContentPlaceHolder1_" + _ColNamesArray[cnt];
                    }
                    else {
                        ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray[cnt];
                    }
                    //----------------------------------------------------////////////////////////

                    var valuedep = $('#' + ctrldep).val();

                    var IsValid = false;



                    for (var loop = 0; loop <= checkboxValues.length - 1; loop++) {


                        if (checkboxValues[loop] == _ColValue) {
                            IsValid = true; break;
                        }
                        else if (checkboxValues[loop] != _ColValue) {
                            IsValid = false;
                        }
                    }



                    if (IsValid == true) {

                        if (_ColTypeArray[cnt] == "3") {
                            $('#' + ctrldep).val('--Select--');
                        }
                        else if (_ColTypeArray[cnt] == "4") {

                            //$('#' + ctrldep + ' input').prop('checked', false);
                            //$('#' + ctrldep + ' input').prop('disabled', true);
                            //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                        }
                        else {
                            $('#' + ctrldep).val('');
                        }

                        if (_Operation == "Disable") {



                            $('#' + ctrldep).attr('disabled', true);
                            if (_ColTypeArray[cnt] == "4") {
                                $('#' + ctrldep + ' input').prop('checked', false);
                                $('#' + ctrldep + ' input').prop('disabled', true);
                            }

                        }
                        else if (_Operation == "Enable") {


                            $('#' + ctrldep).attr('disabled', false);
                            if (_ColTypeArray[cnt] == "4") {
                                //$('#' + ctrldep + ' input').prop('checked', false);
                                $('#' + ctrldep + ' input').prop('disabled', false);
                            }
                        }
                    }
                    else if (IsValid == false) {

                        if (_ColTypeArray[cnt] == "3") {
                            $('#' + ctrldep).val('--Select--');
                        }
                        else if (_ColTypeArray[cnt] == "4") {

                            //$('#' + ctrldep + ' input').prop('checked', false);
                            //$('#' + ctrldep + ' input').prop('disabled', false);
                            //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                        }

                        else {
                            $('#' + ctrldep).val('');
                        }

                        if (_Operation == "Disable") {


                            $('#' + ctrldep).attr('disabled', false);
                            if (_ColTypeArray[cnt] == "4") {
                                //$('#' + ctrldep + ' input').prop('checked', false);
                                $('#' + ctrldep + ' input').prop('disabled', false);
                            }

                        }
                        else if (_Operation == "Enable") {


                            $('#' + ctrldep).attr('disabled', true);
                            if (_ColTypeArray[cnt] == "4") {
                                $('#' + ctrldep + ' input').prop('checked', false);
                                $('#' + ctrldep + ' input').prop('disabled', true);
                            }
                        }
                    }


                }


            }
            else if (!result) {

                var _ColNamesArray = _ColNames;

                var _ColTypeArray = _ColType;

                var ctrldep = "";
                //----------------------Checking control type-----------------------------------------------------///
                if (_ColTypeArray == "cal" || _ColTypeArray == "Time") {

                    ctrldep = "ctl00_ContentPlaceHolder1_" + _ColNamesArray;
                }
                else {
                    ctrldep = 'ContentPlaceHolder1_' + _ColNamesArray;
                }
                //----------------------------------------------------////////////////////////

                var valuedep = $('#' + ctrldep).val();

                var IsValid = false;
                for (var loop = 0; loop <= checkboxValues.length - 1; loop++) {

                    if (checkboxValues[loop] == _ColValue) {
                        IsValid = true; break;
                    }
                    else if (checkboxValues[loop] != _ColValue) {
                        IsValid = false;
                    }
                }


                if (IsValid == true) {


                    if (_ColTypeArray == "3") {

                        $('#' + ctrldep).val('--Select--');
                    }

                    else if (_ColTypeArray == "4") {

                        // $('#' + ctrldep + ' input').prop('checked', false);
                        // $('#' + ctrldep + ' input').prop('disabled', true);
                        //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                    }
                    else {

                        $('#' + ctrldep).val('');
                    }


                    if (_Operation == "Disable") {

                        //$('#' + ctrldep).val('');
                        $('#' + ctrldep).attr('disabled', true);
                        if (_ColTypeArray == "4") {

                            $('#' + ctrldep + ' input').prop('checked', false);
                            $('#' + ctrldep + ' input').prop('disabled', true);
                        }

                    }
                    else if (_Operation == "Enable") {
                        // $('#' + ctrldep).val('');
                        $('#' + ctrldep).attr('disabled', false);
                        if (_ColTypeArray == "4") {
                            //$('#' + ctrldep + ' input').prop('checked', false);
                            $('#' + ctrldep + ' input').prop('disabled', false);
                        }
                    }
                }
                else if (IsValid == false) {

                    if (_ColTypeArray == "3") {

                        $('#' + ctrldep).val('--Select--');
                    }
                    else if (_ColTypeArray == "4") {

                        //$('#' + ctrldep + ' input').prop('checked', false);
                        //$('#' + ctrldep + ' input').prop('disabled', false);
                        //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                    }
                    else {

                        $('#' + ctrldep).val('');
                    }

                    if (_Operation == "Disable") {

                        //$('#' + ctrldep).val('');
                        $('#' + ctrldep).attr('disabled', false);
                        if (_ColTypeArray == "4") {
                            //$('#' + ctrldep + ' input').prop('checked', false);
                            $('#' + ctrldep + ' input').prop('disabled', false);
                        }

                    }
                    else if (_Operation == "Enable") {

                        // $('#' + ctrldep).val('');
                        $('#' + ctrldep).attr('disabled', true);
                        if (_ColTypeArray == "4") {
                            $('#' + ctrldep + ' input').prop('checked', false);
                            $('#' + ctrldep + ' input').prop('disabled', true);
                        }
                    }
                }

            }

            return true;
        }

    </script>

    <script type="text/javascript">
        function PostBackControlCheck() {

            var oTable = JSON.parse($("#ContentPlaceHolder1_hdnpostback").val());
         
            for (var i = 0; i < oTable.length; i++) {


                var Name = oTable[i]["fldQnEngText"];
                var type = oTable[i]["fldQnWidgetId"];
                var ctrlName = oTable[i]["fldQnWidgetName"];
                var Mandatory = oTable[i]["fldQnMandatory"];
                var IsDependent = oTable[i]["fldIsDependent"];
                var DependentColNames = oTable[i]["fldDependentColNames"];
                var DependentColValues = oTable[i]["fldDependentColValues"];
                var DependentColType = oTable[i]["fldDependentColType"];
                var IsCrossValidation = oTable[i]["fldIsCrossValidation"];
                var Operation = oTable[i]["fldOperation"];
                var ColValue = oTable[i]["fldValue"];
                var ColType = oTable[i]["fldColType"];
                var ColToEnableOrDisable = oTable[i]["fldColToEnableOrDisable"];
                var Qid = oTable[i]["fldQnId"];



                var ctrl = "";
                //----------------------Checking control type-----------------------------------------------------///
                if (type == "cal" || type == "Time") {

                    ctrl = "ctl00_ContentPlaceHolder1_" + ctrlName;
                }
                else {
                    ctrl = 'ContentPlaceHolder1_' + ctrlName;
                }

                var value = $('#' + ctrl).val();



                if (type == "1") {
                    value = value.trim();
                }
                else if (type == "4") {



                    var selectedItems = "";
                    $('#' + ctrl + ' input:checkbox:checked').each(function () {
                        selectedItems = $(this).next().html();

                    });


                    //alert($('#' + ctrl + ' input:checkbox:checked').serialize());


                    var checkboxValues = [];
                    $('#' + ctrl + ' input:checkbox:checked').map(function () {
                        checkboxValues.push($(this).val());
                    });

                    value = checkboxValues;


                }


                if (IsCrossValidation == "1" || IsCrossValidation.toString().toUpperCase() == "TRUE") {



                    if (type != "4") {



                        var oTableChild = JSON.parse($("#ContentPlaceHolder1_hdnControlChild").val());


                        for (var ii = 0; ii < oTableChild.length; ii++) {


                            var fldQnId = oTableChild[ii]["fldQnId"];
                            var fldFormId = oTableChild[ii]["fldFormId"];
                            var fldIsCrossValidation = oTableChild[ii]["fldIsCrossValidation"];
                            var fldOperation = oTableChild[ii]["fldOperation"];
                            var fldValue = oTableChild[ii]["fldValue"];
                            var fldColType = oTableChild[ii]["fldColType"];
                            var fldColToEnableOrDisable = oTableChild[ii]["fldColToEnableOrDisable"];
                            var fldIsNotEmptyValue = oTableChild[ii]["fldIsNotEmptyValue"];



                            if (Qid == fldQnId && IsCrossValidation.toString().toUpperCase() == fldIsCrossValidation.toString().toUpperCase()) {//&& (fldValue == value || fldIsNotEmptyValue==true )


                                ColToEnableOrDisable = fldColToEnableOrDisable;
                                ColValue = fldValue;
                                ColType = fldColType;
                                Operation = fldOperation;


                                // break;
                            }

                            if (ColToEnableOrDisable != 'null' && ColToEnableOrDisable != null) {


                                 var result = ((ColToEnableOrDisable.indexOf(",") == -1) ? false : true);


                                if (result) {


                                    var colnames = new Array();
                                    if (ColToEnableOrDisable != "") {
                                        colnames = ColToEnableOrDisable.split(",");
                                    }

                                    var coltype = new Array();
                                    if (ColType != "") {
                                        coltype = ColType.split(",");
                                    }

                                    var IsChk = false;




                                    var IsChkNext = false;
                                    if (fldIsNotEmptyValue == true) {
                                        IsChkNext = true;

                                    }

                                    if (type == "4") {

                                        for (var loop = 0; loop <= value.length - 1; loop++) {


                                            if (value[loop] == ColValue) {
                                                IsChk = true; break;
                                            }
                                            else if (value[loop] != ColValue) {
                                                IsChk = false;
                                            }
                                        }

                                    }
                                    else if (type == "3") {

                                        if (value == ColValue || (IsChkNext == true && ColValue == "" && value != "--Select--")) {

                                            IsChk = true;
                                        }

                                    }
                                    else if (type == "1") {

                                        if ((ColValue == value && IsChkNext == false) || (IsChkNext == true && ColValue == "" && value != "")) {

                                            IsChk = true;
                                        }



                                    }
                                    else {
                                        if (value == ColValue) {
                                            IsChk = true;
                                        }
                                    }


                                    var cnt = 0;
                                    for (cnt = 0; cnt < colnames.length; cnt++) {

                                        var ctrldep = "";
                                        //----------------------Checking control type-----------------------------------------------------///
                                        if (coltype[cnt] == "cal" || coltype[cnt] == "Time") {

                                            ctrldep = "ctl00_ContentPlaceHolder1_" + colnames[cnt];
                                        }
                                        else {
                                            ctrldep = 'ContentPlaceHolder1_' + colnames[cnt];
                                        }
                                        //----------------------------------------------------////////////////////////

                                        var valuedep = $('#' + ctrldep).val();



                                        if (IsChk) {

                                            if (coltype == "3") {

                                                //$('#' + ctrldep).val('--Select--');
                                            }

                                            else if (coltype[cnt] == "4") {

                                                // $('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', true);
                                                //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                            }
                                            else {

                                                //$('#' + ctrldep).val('');
                                            }

                                            if (Operation == "Disable") {

                                                //$('#' + ctrldep).val('');
                                                $('#' + ctrldep).attr('disabled', true);
                                                if (coltype[cnt] == "4") {
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                                }

                                            }
                                            else if (Operation == "Enable") {

                                                $('#' + ctrldep).attr('disabled', false);
                                                if (coltype[cnt] == "4") {
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                                }
                                            }
                                        }
                                        else if (!IsChk) {

                                            if (coltype[cnt] == "3") {
                                                //$('#' + ctrldep).val('--Select--');
                                            }
                                            else if (coltype[cnt] == "4") {

                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', false);
                                                //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                            }

                                            else {
                                                // $('#' + ctrldep).val('');
                                            }

                                            if (Operation == "Disable") {


                                                $('#' + ctrldep).attr('disabled', false);
                                                if (coltype[cnt] == "4") {
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                                }

                                            }
                                            else if (Operation == "Enable") {


                                                $('#' + ctrldep).attr('disabled', true);
                                                if (coltype[cnt] == "4") {
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                                }
                                            }

                                        }

                                    }


                                }
                                else if (!result) {


                                    var colnames = ColToEnableOrDisable;

                                    var coltype = ColType;

                                    var IsChk = false;

                                    var IsChkNext = false;
                                    if (fldIsNotEmptyValue == true) {
                                        IsChkNext = true;

                                    }

                                    //alert(fldIsNotEmptyValue); alert(IsChkNext);



                                    if (type == "3") {
                                        if (value == ColValue || (IsChkNext == true && ColValue == "" && value != "--Select--")) {

                                            IsChk = true;
                                        }
                                    }
                                    else if (type == "1") {
                                        if ((ColValue == value && IsChkNext == false) || (IsChkNext == true && ColValue == "" && value != "")) {

                                            IsChk = true;
                                        }
                                    }

                                    else if (type == "4") {

                                        for (var loop = 0; loop <= value.length - 1; loop++) {


                                            if (value[loop] == ColValue) {
                                                IsChk = true; break;
                                            }
                                            else if (value[loop] != ColValue) {
                                                IsChk = false;
                                            }
                                        }

                                    }
                                    else {
                                        if (value == ColValue) {
                                            IsChk = true;
                                        }
                                    }



                                    var ctrldep = "";
                                    //----------------------Checking control type-----------------------------------------------------///
                                    if (coltype == "cal" || coltype == "Time") {

                                        ctrldep = "ctl00_ContentPlaceHolder1_" + colnames;
                                    }
                                    else {
                                        ctrldep = 'ContentPlaceHolder1_' + colnames;
                                    }
                                    //----------------------------------------------------////////////////////////

                                    var valuedep = $('#' + ctrldep).val();

                                    if (IsChk) {

                                        if (coltype == "3") {

                                            //$('#' + ctrldep).val('--Select--');
                                        }

                                        else if (coltype == "4") {

                                            //$('#' + ctrldep + ' input').prop('checked', false);
                                            $('#' + ctrldep + ' input').prop('disabled', true);
                                            //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                        }
                                        else {

                                            //$('#' + ctrldep).val('');
                                        }

                                        if (Operation == "Disable") {

                                            //$('#' + ctrldep).val('');
                                            $('#' + ctrldep).attr('disabled', true);
                                            if (coltype == "4") {
                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', true);
                                            }

                                        }
                                        else if (Operation == "Enable") {

                                            $('#' + ctrldep).attr('disabled', false);
                                            if (coltype == "4") {
                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', false);
                                            }
                                        }
                                    }
                                    else if (!IsChk) {

                                        if (coltype == "3") {
                                            //$('#' + ctrldep).val('--Select--');
                                        }
                                        else if (coltype == "4") {

                                            //$('#' + ctrldep + ' input').prop('checked', false);
                                            $('#' + ctrldep + ' input').prop('disabled', false);
                                            //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                        }

                                        else {
                                            //$('#' + ctrldep).val('');
                                        }

                                        if (Operation == "Disable") {


                                            $('#' + ctrldep).attr('disabled', false);
                                            if (coltype == "4") {
                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', false);
                                            }

                                        }
                                        else if (Operation == "Enable") {


                                            $('#' + ctrldep).attr('disabled', true);
                                            if (coltype == "4") {
                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                $('#' + ctrldep + ' input').prop('disabled', true);
                                            }
                                        }

                                    }

                                }
                            }

                        }


                    }
                    else if (type == "4") {



                        for (var loop = 0; loop <= value.length - 1; loop++) {


                            var oTableChild = JSON.parse($("#ContentPlaceHolder1_hdnControlChild").val());

                            for (var ii = 0; ii < oTableChild.length; ii++) {


                                var fldQnId = oTableChild[ii]["fldQnId"];
                                var fldFormId = oTableChild[ii]["fldFormId"];
                                var fldIsCrossValidation = oTableChild[ii]["fldIsCrossValidation"];
                                var fldOperation = oTableChild[ii]["fldOperation"];
                                var fldValue = oTableChild[ii]["fldValue"];
                                var fldColType = oTableChild[ii]["fldColType"];
                                var fldColToEnableOrDisable = oTableChild[ii]["fldColToEnableOrDisable"];
                                var fldIsNotEmptyValue = oTableChild[ii]["fldIsNotEmptyValue"];


                                if (Qid == fldQnId && IsCrossValidation.toString().toUpperCase() == fldIsCrossValidation.toString().toUpperCase()) {//&& fldValue == value[loop]


                                    ColToEnableOrDisable = fldColToEnableOrDisable;
                                    ColValue = fldValue;
                                    ColType = fldColType;
                                    Operation = fldOperation;

                                    // break;
                                }

                                if (ColToEnableOrDisable != 'null' && ColToEnableOrDisable != null) {

                                     var result = ((ColToEnableOrDisable.indexOf(",") == -1) ? false : true);


                                    if (result) {


                                        var colnames = new Array();
                                        if (ColToEnableOrDisable != "") {
                                            colnames = ColToEnableOrDisable.split(",");
                                        }

                                        var coltype = new Array();
                                        if (ColType != "") {
                                            coltype = ColType.split(",");
                                        }

                                        var IsChk = false;

                                        var IsChkNext = false;
                                        if (fldIsNotEmptyValue == true) {
                                            IsChkNext = true;

                                        }

                                        if (type == "4") {




                                            if (value[loop] == ColValue) {


                                                IsChk = true; //break;
                                            }
                                            else if (value[loop] != ColValue) {
                                                IsChk = false;
                                            }


                                        }



                                        var cnt = 0;
                                        for (cnt = 0; cnt < colnames.length; cnt++) {


                                            var ctrldep = "";
                                            //----------------------Checking control type-----------------------------------------------------///
                                            if (coltype[cnt] == "cal" || coltype[cnt] == "Time") {

                                                ctrldep = "ctl00_ContentPlaceHolder1_" + colnames[cnt];
                                            }
                                            else {
                                                ctrldep = 'ContentPlaceHolder1_' + colnames[cnt];
                                            }
                                            //----------------------------------------------------////////////////////////

                                            var valuedep = $('#' + ctrldep).val();


                                            if (IsChk) {

                                                if (coltype[cnt] == "3") {

                                                    // $('#' + ctrldep).val('--Select--');
                                                }

                                                else if (coltype[cnt] == "4") {

                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    //$('#' + ctrldep + ' input').prop('disabled', true);
                                                    //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                                }
                                                else {

                                                    //$('#' + ctrldep).val('');
                                                }

                                                if (Operation == "Disable") {

                                                    //$('#' + ctrldep).val('');
                                                    $('#' + ctrldep).attr('disabled', true);
                                                    if (coltype[cnt] == "4") {
                                                        $('#' + ctrldep + ' input').prop('disabled', true);
                                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                                    }

                                                }
                                                else if (Operation == "Enable") {

                                                    $('#' + ctrldep).attr('disabled', false);
                                                    if (coltype[cnt] == "4") {
                                                        $('#' + ctrldep + ' input').prop('disabled', false);
                                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                                    }
                                                }
                                            }
                                            else if (!IsChk) {


                                                if (coltype[cnt] == "3") {
                                                    //$('#' + ctrldep).val('--Select--');
                                                }
                                                else if (coltype[cnt] == "4") {

                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                    //$('#' + ctrldep + ' input').prop('disabled', false);
                                                    //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                                }

                                                else {
                                                    $('#' + ctrldep).val('');
                                                }

                                                if (Operation == "Disable") {


                                                    $('#' + ctrldep).attr('disabled', false);
                                                    if (coltype[cnt] == "4") {
                                                        $('#' + ctrldep + ' input').prop('disabled', true);
                                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                                    }

                                                }
                                                else if (Operation == "Enable") {


                                                    $('#' + ctrldep).attr('disabled', true);
                                                    if (coltype[cnt] == "4") {
                                                        $('#' + ctrldep + ' input').prop('disabled', false);
                                                        //$('#' + ctrldep + ' input').prop('checked', false);
                                                    }
                                                }

                                            }

                                        }


                                    }
                                    else if (!result) {


                                        var colnames = ColToEnableOrDisable;

                                        var coltype = ColType;

                                        var IsChk = false;

                                        var IsChkNext = false;
                                        if (fldIsNotEmptyValue == true) {
                                            IsChkNext = true;

                                        }

                                        if (type == "4") {




                                            if (value[loop] == ColValue) {
                                                IsChk = true; //break;
                                            }
                                            else if (value[loop] != ColValue) {
                                                IsChk = false;
                                            }


                                        }



                                        var ctrldep = "";
                                        //----------------------Checking control type-----------------------------------------------------///
                                        if (coltype == "cal" || coltype == "Time") {

                                            ctrldep = "ctl00_ContentPlaceHolder1_" + colnames;
                                        }
                                        else {
                                            ctrldep = 'ContentPlaceHolder1_' + colnames;
                                        }
                                        //----------------------------------------------------////////////////////////

                                        var valuedep = $('#' + ctrldep).val();

                                        if (IsChk) {

                                            if (coltype == "3") {

                                                //$('#' + ctrldep).val('--Select--');
                                            }

                                            else if (coltype == "4") {

                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                //$('#' + ctrldep + ' input').prop('disabled', true);
                                                //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                            }
                                            else {

                                                //$('#' + ctrldep).val('');
                                            }

                                            if (Operation == "Disable") {

                                                //$('#' + ctrldep).val('');
                                                $('#' + ctrldep).attr('disabled', true);
                                                if (coltype == "4") {
                                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                }

                                            }
                                            else if (Operation == "Enable") {

                                                $('#' + ctrldep).attr('disabled', false);
                                                if (coltype == "4") {
                                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                }
                                            }
                                        }
                                        else if (!IsChk) {

                                            if (coltype == "3") {
                                                //$('#' + ctrldep).val('--Select--');
                                            }
                                            else if (coltype == "4") {

                                                //$('#' + ctrldep + ' input').prop('checked', false);
                                                //$('#' + ctrldep + ' input').prop('disabled', false);
                                                //$('#' + ctrldep + ' input').not(item).prop('checked', false);
                                            }

                                            else {
                                                //$('#' + ctrldep).val('');
                                            }

                                            if (Operation == "Disable") {


                                                $('#' + ctrldep).attr('disabled', false);
                                                if (coltype == "4") {
                                                    $('#' + ctrldep + ' input').prop('disabled', true);
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                }

                                            }
                                            else if (Operation == "Enable") {


                                                $('#' + ctrldep).attr('disabled', true);
                                                if (coltype == "4") {
                                                    $('#' + ctrldep + ' input').prop('disabled', false);
                                                    //$('#' + ctrldep + ' input').prop('checked', false);
                                                }
                                            }

                                        }

                                    }
                                }

                            }




                        }

                    }




                }

            }

            Focus();


            return true;

        }

    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


    <div id="dialog" style="display: none;"></div>

    <div style="display: none;">
        <asp:HiddenField ID="hdnControl" runat="server" />
        <asp:HiddenField ID="hdnpostback" runat="server" />
        <asp:HiddenField ID="hdnControlChild" runat="server" />
        <asp:HiddenField ID="hdnFrmCtrlDetails" runat="server" />

    </div>


    <div class="loading" align="center" style="display: none;">
        <div class="imgCenter">

            <%-- <img class="" src="../img/progress.gif" alt="" />
            <h2 style="color: #fff;">Processing Data
                <br />
                Please Wait...</h2>--%>
            <div class="loader">Loading...</div>

        </div>
    </div>



    <asp:HiddenField ID="hfkeypress" runat="server" Value="0" />


    <h3 class="text-center">Pre - Training Survey</h3>
    <div class="scrolltop">

        <%--<div class="well well-small">

            <h3 id="formNameH" runat="server"></h3>

        </div>--%>

        <div class="container">
            <div class="panelMain">
                <div class="PanelBbody">



                    <%--  <fieldset class="scheduler-border">
                            <strong></strong>
                        </fieldset>--%>
                    <%-- <fieldset class="scheduler-border" runat="server" id="fsheader">
                            <div class="panel panel-success fst_pannel">
                           </div>--%>
                    <%-- <legend class="scheduler-border">Section 1: Identification Particulars </legend>

                            <!-- panel section  -->

                            <div class="">
                                <div class="">
                                    <h4 class="text-center"></h4>
                                </div>
                                <div class="">
                                    <div class="col-md-4 col-sm-4 ">
                                        <div class="form-group">

                                            a href="sectionone.html" class="SectionOne SectionHover">Section I</a>
                                            <asp:Button ID="btnsec1" runat="server" Text="Section I" class="BtnSection" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4 ">
                                        <div class="form-group">

                                            <a href="sectiontwo.html" class="SectionHover">Section II</a>

                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4 ">
                                        <div class="form-group">

                                            <a href="sectionthree.html" class="SectionHover">Section III</a>

                                        </div>
                                    </div>
                                </div>
                                <!-- Panel Body -->
                            </div>--%>
                    <!-- panel End -->

                    <%-- <strong></strong>
                        </fieldset>--%>

                    <!-- panel section  -->



                    <asp:UpdatePanel runat="server" ID="upmain">
                        <ContentTemplate>

                            <asp:HiddenField ID="hfGridDetails" runat="server" />



                            <fieldset class="scheduler-border" id="fscontent" runat="server">
                                <%--<div class="col-md-12 col-sm-12">
                                        <div class="col-md-12 col-sm-12 ">
                                             <div class="clearfix">&nbsp;</div>
                                            <h2 class="h2head">Patient Engagement</h2>
                                            <div class="clearfix">&nbsp;</div>
                                        </div>
                                       
                                    </div>--%>
                                <asp:HiddenField ID="hffocus" runat="server" />
                                <div id="divmain" class="panel panel-success" runat="server">

                                    <div class="panel-heading">
                                        <h3 id="formNameH" runat="server"></h3>
                                    </div>

                                    <%--<div class="panel-body PanelBg">

                                            <div class="col-md-4 col-sm-4 ">


                                                <div class="clearfix"></div>
                                            </div>
                                        </div>--%>
                                </div>


                            </fieldset>






                            <fieldset class="scheduler-border">

                                <div class="form-group">
                                    <div class="col-md-12">

                                        <fieldset>
                                            <div class="col-md-4">
                                                <div class="col-md-4">
                                                    <asp:Button runat="server" ID="btnBack" Style="min-width: 130px; margin-top: auto" Text="Back" class="btn btnCLass btn-large btn-success" OnClick="btnBack_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-4"></div>
                                            <div class="col-md-2 col-md-offset-2">
                                                <%--<input type="submit" name="SaveBtn" value="Save" i="" class="btn btnCLass btn-large btn-success">--%>

                                                <asp:Button runat="server" ID="btnsave" Style="min-width: 130px; margin-top: auto" Text="Next" class="btn btnCLass btn-large btn-success" OnClientClick="return Validation(0);" OnClick="btnsave_Click" />

                                                <%--<asp:Button runat="server" ID="btnaddtolist" Text="AddToList" class="btn btnCLass btn-large btn-success" OnClick="btnaddtolist_Click"  />--%>
                                            </div>
                                            <%-- <br />--%>
                                            <%-- <br />--%>
                                            <%-- <br />
                                            <br />--%>
                                        </fieldset>
                                    </div>
                                </div>


                            </fieldset>

                        </ContentTemplate>
                    </asp:UpdatePanel>



                </div>
            </div>

        </div>
        <br />
        <br />
        <br />
        <br />
    </div>



</asp:Content>
