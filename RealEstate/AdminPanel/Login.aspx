<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>


<%@ Import Namespace="Utility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- End Required meta tags -->
    <!-- Begin SEO tag -->

    <title><%= "Admin Login - " + new BAL.GeneralSettings().getConfigValue("WebsiteName").ToString()%></title>
    <link rel="shortcut icon" href="assets/favicon.ico" />
    <meta name="theme-color" content="#3063A0" />
    <!-- Google font -->
    <link href="https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600" rel="stylesheet" />
    <!-- End Google font -->
    <!-- BEGIN PLUGINS STYLES -->
    <link rel="stylesheet" href="assets_1/vendor/%40fortawesome/fontawesome-free/css/all.min.css" />
    <link href="js/jquery-alerts/jquery.alerts.css" rel="stylesheet" />
    <!-- END PLUGINS STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link rel="stylesheet" href="assets_1/stylesheets/theme.min.css" data-skin="default" />
    <link rel="stylesheet" href="assets_1/stylesheets/custom.css" />
    <link href="css/dev.css" rel="stylesheet" />
    <style>
        #popup_message {
            font-family:-apple-system,BlinkMacSystemFont,Fira Sans,Helvetica Neue,Apple Color Emoji,sans-serif;
        }
    </style>
</head>
<body class="bg-med" style="background-color: #dce1eb">

    <%-- <div id="login" class="container">
        <div class="row">
            <div class="center-panel" style="margin-top:10%">
                <div class="col-md-3 col-sm-2 col-xs-1"></div>
                <div class="col-md-6 col-sm-8 col-xs-10 text-center">

                    <div class="custom-check animated fadeInDown">
                        <span class="padding-bottom avatar avatar-md">
                            <img src="images/DRP-logo.png" alt="logo" height="150" style="" class="img-circle1" /></span>
                        <br />
                        <span style="color: #f0653a;font-size: 26px;margin-bottom:10px;">Daily Progress Report</span>
                        <div class="clearfix"></div>
                        <br />
                        <div class="notification n-success" id="divMsg" runat="server" style="display: none;">Please log in.</div>
                        <div id="divUserLogin">
                            <form class="form-signin form-group" id="frmUser" onsubmit="PostForm(); return false;" method="post">
                                <div class="input-stacked input-group-lg">
                                    <input type="text" class="form-control" style="border-radius:0px;margin-bottom:13px;" placeholder="Email Address" id="tbxUsername" name="tbxUsername" autofocus autocomplete="off" />
                                    <input type="password" class="form-control" style="border-radius:0px;" id="tbxPassword" name="tbxPassword" placeholder="Password" />
                                </div>
                                <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin" onclick="t='login'">Sign in</button>                                
                            </form>
                        </div>
                        <div id="divUserForgetPwd" style="display:none;">
                            <form id="frmUserForgetPwd" onsubmit="t='forgetpwd';PostForm();return false;"> 
                                <div class="input-stacked input-group-lg">
                                    <input type="text" class="form-control" placeholder="Email Address" style="border-radius:0px;"  id="tbxEmailAddress" name="tbxEmailAddress" maxlength="100" />
                                </div>
                                <button type="submit"  class="btn btn-lg btn-primary btn-block"   id="btnForgotPwd" onclick="t='forgetpwd'" value="Submit" >Submit</button>
                                <div id="progressbar"></div> 
                            </form>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-2 col-xs-1"></div>
                    <div class="clearfix"></div>
                    <br />
                    <a id="lnkLogin" class="text-transparent" style="display:none;" href="javascript:;" onclick="ShowHideCtrl('divUserForgetPwd','divUserLogin');ShowHideCtrl('lnkforgotPass','lnkLogin');">Forgot Password? </a>
                    <a style="display:none;" id="lnkforgotPass" class="text-transparent" href="javascript:;" onclick="ShowHideCtrl('divUserLogin','divUserForgetPwd');ShowHideCtrl('lnkLogin','lnkforgotPass');"> << Back to login </a>
                    <span class="text-transparent">&nbsp;&nbsp;&nbsp;&nbsp;</span>



                </div>
            </div>
        </div>
    </div>--%>

    <main class="auth">
        <header id="auth-header" class="auth-header" style="background-image: url(assets/images/illustration/img-1.png);">
            <h2 style="padding-bottom: 15px;">
                <%--<img class="rounded" src="assets_1/logo.png" alt="" width="100" >--%>                
                RealEstate CRM
            </h2>
        </header>
        <!-- form -->
        <!-- form -->

        <form class="auth-form" id="frmUser" onsubmit="PostForm(); return false;" method="post">
            <div class="mb-4">
                <%--  <div class="mb-3">
                    <img class="rounded" src="assets_1/logo.png" alt="" height="72">
                </div>--%>
                <h1 class="h3">Sign In </h1>
            </div>
            <div class="notification n-success" id="divMsg" runat="server" style="display: none;">Please log in.</div>
            <div class="notification n-success" id="div1" runat="server" style="display: none;">Please log in.</div>
            <div class="clearfix"></div>
            <div id="divLogin">
                <div id="divUserNameDetails">
                    <div class="form-group mb-4">
                        <label class="d-block text-left" for="inputUser">Username</label>
                        <input type="text" class="form-control form-control-lg" placeholder="Email Address" id="tbxUsername" name="tbxUsername" autofocus autocomplete="off" />
                    </div>
                    <!-- /.form-group -->
                    <!-- .form-group -->
                    <div class="form-group mb-4">
                        <label class="d-block text-left" for="inputPassword">Password</label>
                        <input type="password" class="form-control form-control-lg" id="tbxPassword" name="tbxPassword" placeholder="Password" />
                        <a class="pull-right forgotpassword" onclick="OpenForgotPassword('divForgotPassword','divLogin')">Forgot Password?</a>
                    </div>



                    <!-- /.form-group -->
                    <!-- .form-group -->
                    <div class="form-group mb-4">
                        <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin" onclick="t='login'">Sign In</button>

                    </div>
                </div>
                <div id="divOTPDetails" style="display: none">
                    <label style="text-align: left; width: 100%; padding-bottom: 5px;">We have sent OTP in your registered email address</label>
                    <div class="form-group mb-4">
                        <input type="text" class="form-control form-control-lg" placeholder="Enter OTP" id="tbxOTP" name="tbxOTP" autofocus autocomplete="off" onkeypress="return runScript(event)" />
                        <input type="hidden" class="form-control" style="border-radius: 0px; display: none" id="tbxP" name="tbxP" autocomplete="off" />
                        <input type="hidden" class="form-control" style="border-radius: 0px; display: none" id="tbxU" name="tbxU" autocomplete="off" />
                    </div>
                    <!-- /.form-group -->
                    <!-- .form-group -->


                    <!-- /.form-group -->
                    <!-- .form-group -->
                    <div class="form-group mb-4">
                        <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin1" onclick="t='otp'">Submit</button>
                    </div>
                </div>
            </div>
            <div id="divForgotPassword" style="display: none">
                <div class="form-group mb-4 divForgotFormGroup">
                    <label class="d-block text-left">Enter email address below to get password</label>
                    <input type="text" class="form-control form-control-lg email" placeholder="Email Address" id="tbxForgotEmailAddress" name="tbxForgotEmailAddress" autofocus autocomplete="off" onblur="CheckControlStatus()" />
                    <span class="help-block spnForgotEmail" style="display: none">This field is required.</span>
                    <a class="pull-right forgotpassword" onclick="OpenForgotPassword('divLogin','divForgotPassword')"><< Back to Login</a>
                </div>

                <!-- /.form-group -->
                <!-- .form-group -->
                <div class="form-group mb-4 ">
                    <button class="btn btn-lg btn-primary btn-block btnForgotPassword" type="button" onclick="AdminForgotPassword()">Submit</button>
                    
                </div>
            </div>
            <!-- /.form-group -->
            <!-- copyright -->
            <p class="mb-0 px-3 text-muted text-center">
                <%= new BAL.GeneralSettings().getConfigValue("footertext").ToString()%>
            </p>
        </form>

        <!-- /.auth-form -->
        <!-- .auth-announcement -->
        <div id="announcement" class="auth-announcement" style="background-image: url(assets_1/login-bg.png);">
        </div>
        <!-- /.auth-announcement -->
    </main>
    <!-- /.auth -->

    <script src="assets_1/vendor/jquery/jquery.min.js"></script>
    <script src="assets_1/vendor/popper.js/umd/popper.min.js"></script>
    <script src="assets_1/javascript/theme.min.js"></script>
    <script src="assets_1/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.alerts.js"></script>
    <!-- END THEME JS -->

    <!-- Custom -->
    <script src="assets/js/script.js"></script>
    <%--<script src="dist/js/bootstrap.js"></script>--%>
    <script type="text/javascript" src="assets/js/general.js"></script>

    <%-- <script type="text/javascript">
        var divMsg = "<%= divMsg.ClientID %>";
        $('#tbxUsername').focus();
        var t = '';
        function PostForm() {
            //$('#progressbar').html('<div class="progress"><div class="progress-bar progress-bar-success" aria-valuetransitiongoal="100"></div></div>');
            //$('.progress .progress-bar').progressbar(); 
            $('#<%=divMsg.ClientID %>').css('display', 'none');
            if (t == 'login') {
                var err = '';

                if (jQuery.trim($('#tbxUsername').val()) == '' || jQuery.trim($('#tbxUsername').val()) == 'Email Address') {
                    err = '<br />- Email Address is required.';
                }
                else {
                    var a = jQuery.trim($('#tbxUsername').val().replace('<', ''));

                }

                if (jQuery.trim($('#tbxPassword').val()) == '' || jQuery.trim($('#tbxPassword').val()) == 'Password') err += '<br />- Password is required.';

                if (err != '') {
                    if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }
                    msgbox(HeaderText + err, 'Error Message');
                    //DisplMsg(divMsg, HeaderText + err, 'alert-message error')
                    return false;
                }
                $.ajax({

                    url: 'login.aspx?type=' + t,
                    data: $('#frmUser').serialize(),
                    type: 'POST',
                    success: function (resp) {
                        $('#divMsg').html(resp);
                    }

                });

            }
            else {

                var err = '';
                if (jQuery.trim($('#tbxEmailAddress').val()) == '' || jQuery.trim($('#tbxEmailAddress').val()) == 'Email') {
                    err = '<br />- Email';
                }
                else {
                    if (!isValidEmailAddress(jQuery.trim($('#tbxEmailAddress').val()))) {
                        err += '<br/> - Invalid Email';
                    }
                }

                if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }

                $.ajax({
                    url: 'login.aspx?type=' + t,
                    data: $('#frmUserForgetPwd').serialize(),
                    type: 'POST',
                    success: function (resp) { $('#divMsg').html(resp); $('#tbxEmailAddress').val(''); }
                });

            }

            return false;
        }
         function runScript(e) {
            //See notes about 'which' and 'key'
            if (e.keyCode == 13) {
                t = 'otp';
                return false;
            }
        }
    </script>--%>

    <script type="text/javascript">
        var divMsg = "<%= divMsg.ClientID %>";
        $('#tbxUsername').focus();
        var t = '';
        function PostForm() {
            //$('#progressbar').html('<div class="progress"><div class="progress-bar progress-bar-success" aria-valuetransitiongoal="100"></div></div>');
            //$('.progress .progress-bar').progressbar(); 
            $('#<%=divMsg.ClientID %>').css('display', 'none');
            if (t == 'login') {
                var err = '';

                if (jQuery.trim($('#tbxUsername').val()) == '' || jQuery.trim($('#tbxUsername').val()) == 'Email Address') {
                    err = '<br />- Email Address is required.';
                }
                else {
                    var a = jQuery.trim($('#tbxUsername').val().replace('<', ''));

                }

                if (jQuery.trim($('#tbxPassword').val()) == '' || jQuery.trim($('#tbxPassword').val()) == 'Password') err += '<br />- Password is required.';

                if (err != '') {
                    if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }
                    msgbox(HeaderText + err, 'Error Message');
                    //DisplMsg(divMsg, HeaderText + err, 'alert-message error')
                    return false;
                }
                $('#btnLogin').attr('disabled', true);
                $('#imgLoading').show();
                $.ajax({

                    url: 'login.aspx?type=' + t,
                    data: $('#frmUser').serialize(),
                    type: 'POST',
                    success: function (resp) {
                        //$('#divMsg').html(resp);
                        $('#btnLogin').attr('disabled', false);
                        $('#imgLoading').hide();
                        if (resp == 'invalid') {
                            msgbox('<b>Invalid User Name or Password</b>', 'LOGIN'); return false;
                        }
                        else if (resp == 'success') {
                            $('#tbxP').val(jQuery.trim($('#tbxPassword').val()));
                            $('#tbxU').val(jQuery.trim($('#tbxUsername').val()));

                            $('#divUserNameDetails').hide();
                            $('#divOTPDetails').slideToggle('slow');
                        }
                    }

                });

            }
            else if (t == 'otp') {
                var err = '';

                if (jQuery.trim($('#tbxOTP').val()) == '' || jQuery.trim($('#tbxOTP').val()) == 'Enter OTP') err += '<br />- OTP is required.';

                if (err != '') {
                    if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }
                    msgbox(HeaderText + err, 'Error Message');
                    return false;
                }
                $.ajax({

                    url: 'login.aspx?type=' + t,
                    data: $('#frmUser').serialize(),
                    type: 'POST',
                    success: function (resp) {
                        if (resp == 'wrongotp') {
                            msgbox('<b>You have entered Invalid OTP. <br>The maximum retry attempts allowed are 3.</b>', 'LOGIN'); return false;
                        }
                        else if (resp == 'locked') {
                            msgbox('<b>Your account is locked. Kindly contact Administrator.</b>', 'LOGIN');
                            window.setTimeout('RedirectToLoginPage()', 3000);
                            return false;
                        }
                        else {
                            $('#divMsg').html(resp);
                        }
                    }

                });

            }
            else {

                var err = '';
                if (jQuery.trim($('#tbxEmailAddress').val()) == '' || jQuery.trim($('#tbxEmailAddress').val()) == 'Email') {
                    err = '<br />- Email';
                }
                else {
                    if (!isValidEmailAddress(jQuery.trim($('#tbxEmailAddress').val()))) {
                        err += '<br/> - Invalid Email';
                    }
                }

                if (err != '') { msgbox('<b>Please correct the following errors</b>' + err, 'LOGIN'); return false; }

                $.ajax({
                    url: 'login.aspx?type=' + t,
                    data: $('#frmUserForgetPwd').serialize(),
                    type: 'POST',
                    success: function (resp) { $('#divMsg').html(resp); $('#tbxEmailAddress').val(''); }
                });

            }

            return false;
        }
        function RedirectToLoginPage() {
            window.location.href = 'login.aspx'
        }

        function runScript(e) {
            //See notes about 'which' and 'key'
            if (e.keyCode == 13) {
                t = 'otp';
                return false;
            }
        }
        function CheckControlStatus() {
            //if (jQuery.trim($('#tbxForgotEmailAddress').val()) != '') {
            //    $('.spnForgotEmail').hide();
            //    var tbxEmail = $('.divForgotFormGroup .help-block').not('.spnForgotEmail').html();
            //    if (tbxEmail != 'undefined' && tbxEmail != '') {
            //        $('.divForgotFormGroup').addClass('has-error');
            //    }
            //}
        }
        function OpenForgotPassword(divOpenID, divCloseID) {
            $('#' + divOpenID).show();
            $('#' + divCloseID).hide();
            $('#' + divMsg).html('');
            $('#' + divMsg).hide();
        }
          function AdminForgotPassword() {           
            if (jQuery.trim($('#tbxForgotEmailAddress').val()) == '') {
               msgbox('<b>Please enter Email Address</b>', 'Forgot Password'); 
            }
            else {
                
                if (isValidEmailAddress(jQuery.trim($('#tbxForgotEmailAddress').val()))) {

                    $('.btnForgotPassword').attr('disabled', true);
                    $('.btnForgotPassword').html('Loading...');
                    $.ajax({
                        url: 'login.aspx?type=forgetpwd',
                        data: { 'forgotemail': jQuery.trim($('#tbxForgotEmailAddress').val()) },
                        type: 'POST',
                        success: function (resp) {
                            $('.btnForgotPassword').attr('disabled', false);
                            $('.btnForgotPassword').html('Submit');
                            if (resp == 'success') {
                                $('#tbxForgotEmailAddress').val('');                                
                                msgbox('Password has been sent to registered Email Address.', 'Forgot Password');                                 
                            }
                            else if (resp == 'invalid') {
                                msgbox('Invalid Email Address.', 'Forgot Password');                                 
                            }                            
                        }

                    });
                }
                else {
                      msgbox('<b>Please enter valid Email Address</b>', 'Forgot Password'); 
                }
            }
        }

    </script>
</body>
</html>
