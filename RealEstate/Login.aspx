<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Import Namespace="Utility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title><%= new BAL.GeneralSettings().getConfigValue("WebsiteName").ToString()%></title>
    <link href="<%=Config.VirtualDir + "images/favicon.ico"%>" rel="icon" type="image/png" />
    <meta name="theme-color" content="#3063A0" />
    <link href="https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600" rel="stylesheet" />
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/vendor/%40fortawesome/fontawesome-free/css/all.min.css" />
    <%--<link href="js/jquery-alerts/jquery.alerts.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/stylesheets/theme.min.css" data-skin="default" />
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/stylesheets/custom.css" />
    <link href="<%=Config.VirtualDir %>validation-lib/dev.css" rel="stylesheet" />
    <link href="<%=Config.VirtualDir %>style/dev.css" rel="stylesheet" />
</head>
<body class="bg-med" style="background-color: #dce1eb">
    <main class="auth auth-floated">
        <%-- <header id="auth-header" class="auth-header" style="background-image: url(assets/images/illustration/img-1.png);">
            <h1>
                 <img class="rounded" src="<%=Config.VirtualDir %>assets/logo.png" alt="" >
            </h1>            
        </header>--%>
        <!-- form -->
        <!-- .auth-announcement -->
        <%if (BackgroundImage != string.Empty)
            { %>
        <div id="announcement" class="auth-announcement" style="background-image: url(<%=Config.VirtualDir + Config.CMSFiles + BackgroundImage %>);">
        </div>
        <%}
            else
            { %>
        <div id="announcement" class="auth-announcement">
        </div>
        <%} %>
        <!-- form -->
        <form id="form_sample_1" method="post" class="auth-form">
            <%--<form class="auth-form" id="frmUser" onsubmit="PostForm(); return false;" method="post">--%>
            <div class="mb-4">
                <div class="mb-3">
                    <%if (SchoolLogo != string.Empty)
                        { %>
                    <img class="rounded" src="<%=Config.VirtualDir + Config.CMSFiles + SchoolLogo %>" alt="" height="72">
                    <%} %>
                </div>
                <h1 class="h3">Sign In </h1>
            </div>
            <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
            <%--<div class="notification n-success" id="divMsg" runat="server" style="display: none;">Please log in.</div>
            <div class="notification n-success" id="div1" runat="server" style="display: none;">Please log in.</div>--%>
            <div class="clearfix"></div>
            <div id="divLogin">
                <div class="form-group mb-4">
                    <label class="d-block text-left" for="inputUser">Username<span class="red">*</span></label>
                    <input type="text" class="form-control form-control-lg required" placeholder="Email Address" id="tbxUsername" name="tbxUsername" autofocus autocomplete="off" />
                </div>
                <!-- /.form-group -->
                <!-- .form-group -->
                <div class="form-group mb-4">
                    <label class="d-block text-left" for="inputPassword">Password<span class="red">*</span></label>
                    <input type="password" class="form-control form-control-lg required" id="tbxPassword" name="tbxPassword" placeholder="Password" />
                    <a class="pull-right forgotpassword" onclick="OpenForgotPassword('divForgotPassword','divLogin')">Forgot Password?</a>
                </div>
                <!-- /.form-group -->
                <!-- .form-group -->
                <div class="form-group mb-4">
                    <button class="btn btn-lg btn-primary btn-block" type="submit" id="btnLogin" onclick="t='login'">Sign in</button>

                </div>
            </div>


            <div id="divForgotPassword" style="display: none">
                <div class="form-group mb-4 divForgotFormGroup">
                    <label class="d-block text-left">Enter email address below to get temporary password</label>
                    <input type="text" class="form-control form-control-lg email" placeholder="Email Address" id="tbxForgotEmailAddress" name="tbxForgotEmailAddress" autofocus autocomplete="off" onblur="CheckControlStatus()" />
                    <span class="help-block spnForgotEmail" style="display: none">This field is required.</span>
                </div>

                <!-- /.form-group -->
                <!-- .form-group -->
                <div class="form-group mb-4 ">
                    <button class="btn btn-lg btn-primary btn-block btnForgotPassword" type="button" onclick="TeacherForgotPassword()">Submit</button>
                    <a class="pull-right forgotpassword" onclick="OpenForgotPassword('divLogin','divForgotPassword')"><< Back to Login</a>
                </div>
            </div>
            <%-- <label>OR</label>--%>
            <div class="log-divider">
                <span class="bg-light">Sign in with Google</span>
            </div>
            <div class="row">
                <div class="form-group mb-4 col-md-6">
                    <button class="btn btn-lg btn-danger btn-block" type="button" id="btnGoogleLogin" onclick="GoogleLogin()">
                        <i class='fab fa-google mr5'></i>
                        <%--  <img style="height: 20px; vertical-align: text-bottom; margin-right: 8px;" src="<%=Config.VirtualDir %>images/google.png" />--%>
                        Teacher</button>
                </div>
                <div class="form-group mb-4 col-md-6">
                    <button class="btn btn-lg btn-danger btn-block" type="button" id="btnGoogleLoginAdmin" onclick="GoogleLoginAdmin()">
                        <i class='fab fa-google mr5'></i>
                        <%--<img style="height: 20px; vertical-align: text-bottom; margin-right: 8px;" src="<%=Config.VirtualDir %>images/google.png" />--%>
                        Admin</button>
                </div>
            </div>
            DON'T HAVE AN ACCOUNT?<a href="#SignupModal" style="padding-left: 7px;" class="card-footer-item" data-toggle="modal" data-backdrop="static" data-keyboard="false" onclick="ResetControls()">SIGN UP</a>
            <!-- /.form-group -->
            <!-- copyright -->
            <br />
            <br />
            <p class="mb-0 px-3 text-muted text-center">
                <%= new BAL.GeneralSettings().getConfigValue("footertext").ToString()%>
            </p>
        </form>
        <!-- /.auth-form -->


        <!-- /.auth-announcement -->
    </main>
    <div class="modal fade signup" id="SignupModal" tabindex="-1" role="dialog" aria-labelledby="clientContactNewModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h4 class="signupHeading">Sign up with Google</h4>
                    <button type="button" class="btn btn-light btnSignupClose" data-dismiss="modal">X</button>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <form id="form_School_Signup" method="post" class="auth-form1">
                    <div class="modal-body form-row">
                        <div class="col-md-12">
                            <div id="divSignUpMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>


                            <div id="divLocalSignUp" class="row" style="display: none">
                                <!-- .form-group -->
                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">First Name<span class="red">*</span></label>
                                        <input type="text" class="form-control form-control-lg required" placeholder="" id="tbxTeacherFirstName" name="tbxTeacherFirstName" autofocus autocomplete="off" />
                                    </div>
                                </div>
                                <!-- /.form-group -->
                                <!-- .form-group -->
                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">Last Name</label>
                                        <input type="text" class="form-control form-control-lg" placeholder="" id="tbxTeacherLastName" name="tbxTeacherLastName" autofocus autocomplete="off" />
                                    </div>
                                </div>
                                <!-- /.form-group -->
                                <!-- .form-group -->
                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">Email<span class="red">*</span></label>
                                        <input type="text" class="form-control form-control-lg required email" placeholder="" id="tbxTeacherEmail" name="tbxTeacherEmail" autofocus autocomplete="off" />
                                    </div>
                                </div>
                                <!-- /.form-group -->
                                <!-- .form-group -->
                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">Phone No</label>
                                        <input type="text" class="form-control form-control-lg" placeholder="" id="tbxTeacherPhone" name="tbxTeacherPhone" autofocus autocomplete="off" />
                                    </div>
                                </div>

                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">Password<span class="red">*</span></label>
                                        <input id="tbxTeacherPassword" name="tbxTeacherPassword" type="password" autocomplete="off" class="form-control form-control-lg required password" rel="password" minlength="6" maxlength="20" />

                                    </div>
                                </div>

                                <div class="col-md-6 mb-1">
                                    <div class="form-group">
                                        <label class="d-block text-left" for="inputUser">Confirm Password<span class="red">*</span></label>
                                        <input name="tbxTeacherConfirmPassword" id="tbxTeacherConfirmPassword" type="password" maxlength="20" rel="confirmpassword" autocomplete="off" class="form-control form-control-lg required confirmpassword" minlength="6" />

                                    </div>
                                </div>
                                <div class="col-md-12 mb-1">
                                    <button class="btn btn-lg btn-primary  btn-block" type="submit" id="btnSignup" onclick="t='login'">Sign up</button>
                                </div>
                                <br />
                                <div class="col-md-12 mb-1">
                                    <a class="pull-right forgotpassword text-center" style="width: 100%" onclick="HideShowSignup('G')">< Back</a>
                                </div>
                            </div>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.modal-body -->
                    <!-- .modal-footer -->
                    <div class="modal-footer" id="divGoogleSignup">

                        <button type="reset" id="btnSignUpReset" style="display: none"></button>
                        <%--<span class="btn-block text-center">OR</span>--%>

                        <div class="row w100">
                            <div class="form-group mb-2 col-md-6 padleft0">
                                <button class="btn btn-lg btn-danger btn-block" type="button" id="btnGoogleSignUp" onclick="GoogleSignUp()">
                                    <i class='fab fa-google mr5'></i>
                                    <%--<img style="height: 20px; vertical-align: text-bottom; margin-right: 8px;" src="<%=Config.VirtualDir %>images/google.png" />--%>
                                    Teacher</button>
                            </div>
                            <div class="form-group mb-2 col-md-6">
                                <button class="btn btn-lg btn-danger btn-block" type="button" id="btnGoogleSignUpAdmin" onclick="GoogleSignUpAdmin()">
                                    <i class='fab fa-google mr5'></i>
                                    <%--<img style="height: 20px; vertical-align: text-bottom; margin-right: 8px;" src="<%=Config.VirtualDir %>images/google.png" />--%>
                                    Admin</button>
                            </div>
                        </div>
                      <%--  <div class="log-divider w100">
                            <span class="bg-light">Or</span>
                        </div>
                        <div class="col-md-12 padleft0">
                            <div class="col-md-12">
                                <button class="btn btn-lg btn-primary btn-block" type="button" onclick="HideShowSignup('L')">Sign up with Local Account</button>
                            </div>
                        </div>--%>
                        <br />
                    </div>
                </form>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.auth -->
    <a href="#thankyouModal" id="lnkThankyou" style="display: none" data-toggle="modal" data-backdrop="static" data-keyboard="false">&nbsp;</a>
    <a href="#thankyouGoogleModal" id="lnkGoogleThankyou" style="display: none" data-toggle="modal" data-backdrop="static" data-keyboard="false">&nbsp;</a>

    <div class="modal fade thankyou" id="thankyouModal" tabindex="-1" role="dialog" aria-labelledby="clientContactNewModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h4>Sign Up</h4>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->

                <div class="modal-body form-row">
                    Thank you for signup. Our team will contact you soon.
                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                </div>

                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade thankyou" id="thankyouGoogleModal" tabindex="-1" role="dialog" aria-labelledby="clientContactNewModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h4><%=PopupHeading %></h4>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->

                <div class="modal-body form-row">
                    <%=PopupMessage %>
                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                </div>

                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script src="<%=Config.VirtualDir %>assets/vendor/jquery/jquery.min.js"></script>
    <script src="<%=Config.VirtualDir %>assets/vendor/popper.js/umd/popper.min.js"></script>
    <script src="<%=Config.VirtualDir %>assets/javascript/theme.min.js"></script>
    <script src="<%=Config.VirtualDir %>assets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="<%=Config.VirtualDir %>js/jquery.validate.min.js"></script>
    <script src="<%=Config.VirtualDir %>js/form-validation.js"></script>
    <script src="<%=Config.VirtualDir %>js/general.js"></script>
    <%--   <script src="assets/js/jquery.alerts.js"></script>--%>
    <!-- END THEME JS -->

    <!-- Custom -->
    <%--<script src="assets/js/script.js"></script>    
    <script type="text/javascript" src="assets/js/general.js"></script>--%>

    <script type="text/javascript">
        if (parseInt('<%=showsuccesspopup %>') == 1) {
            $('#lnkGoogleThankyou').click()[0];
        }

        var divMsg = "<%= divMsg.ClientID %>";
        var divSignUpMsg = "<%= divSignUpMsg.ClientID %>";

        FormValidation.init();
        $('#tbxUsername').focus();

        function ValidateForm() {
            $.ajax({
                url: '<%=Config.VirtualDir + RequestedSchoolURL %>/login.aspx?type=login',
                data: $('#form_sample_1').serialize(),
                type: 'POST',
                success: function (resp) {
                    if (resp == 'success') {
                        window.location.href = '<%=Config.VirtualDir + RequestedSchoolURL %>/dashboard.aspx';
                    }
                    else if (resp == 'deactivated') {
                        DisplMsg(divMsg, 'Your account is deactivated. Kindly contact administrator for more details.', 'alert-message error');
                    }
                    else if (resp == 'notapproved') {
                        DisplMsg(divMsg, 'Your account is under approval process.', 'alert-message error');
                    }
                    else {
                        DisplMsg(divMsg, 'Invalid Username or Password.', 'alert-message error');
                    }
                }

            });
        }

        function GoogleLogin() {
            var redirection_url = '<%=Config.WebSiteUrl %>google-login.aspx';
            window.location.href = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=<%=Config.GoogleClientID%>";
        }
        function GoogleLoginAdmin() {
            var redirection_url = '<%=Config.WebSiteUrl %>google-login-admin.aspx';
            window.location.href = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=<%=Config.GoogleClientID%>";
        }
        function ResetControls() {
            $('#btnSignUpReset').click();
            $('.form-group').removeClass('has-error');
            $('.help-block').html('');

            $('#divGoogleSignup').show();
            $('#divLocalSignUp').hide();
        }
        function ValidateSchoolSignUp() {
            $('#btnSignup').attr('disabled', true);
            $('#btnCompare').html('Loading...');
            $.ajax({
                url: '<%=Config.VirtualDir + RequestedSchoolURL %>/login.aspx?type=signup',
                data: $('#form_School_Signup').serialize(),
                type: 'POST',
                success: function (resp) {
                    $('#btnSignup').attr('disabled', false);
                    $('#btnCompare').html('Sign up');
                    if (resp == 'success') {
                        ResetControls();
                        $('.btnSignupClose').click();
                        $('#lnkThankyou').click()[0];
                    }
                    else if (resp == 'duplicate') {
                        DisplMsg(divSignUpMsg, 'This email is already registered in our school.', 'alert-message error');
                    }
                }

            });
        }
        function GoogleSignUp() {
            var redirection_url = '<%=Config.WebSiteUrl %>google-signup.aspx';
            window.location.href = "https://accounts.google.com/o/oauth2/v2/auth?scope=email&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=<%=Config.GoogleClientID%>";
        }
        function GoogleSignUpAdmin() {
            var redirection_url = '<%=Config.WebSiteUrl %>google-signup-admin.aspx';
            window.location.href = "https://accounts.google.com/o/oauth2/v2/auth?scope=email&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=<%=Config.GoogleClientID%>";
        }


        function OpenForgotPassword(divOpenID, divCloseID) {
            $('#' + divOpenID).show();
            $('#' + divCloseID).hide();
            $('#' + divMsg).html('');
            $('#' + divMsg).hide();
        }

        function TeacherForgotPassword() {
            $('.divForgotFormGroup').removeClass('has-error')
            $('.spnForgotEmail').hide();

            if (jQuery.trim($('#tbxForgotEmailAddress').val()) == '') {
                $('.divForgotFormGroup').addClass('has-error');
                $('.spnForgotEmail').show();
            }
            else {
                var tbxEmail = $('.divForgotFormGroup .help-block').not('.spnForgotEmail').html();
                if (tbxEmail == 'undefined' || tbxEmail == '') {

                    $('.btnForgotPassword').attr('disabled', true);
                    $('.btnForgotPassword').html('Loading...');
                    $.ajax({
                        url: '<%=Config.VirtualDir + RequestedSchoolURL %>/login.aspx?type=forgetpwd',
                        data: { 'forgotemail': jQuery.trim($('#tbxForgotEmailAddress').val()) },
                        type: 'POST',
                        success: function (resp) {
                            $('.btnForgotPassword').attr('disabled', false);
                            $('.btnForgotPassword').html('Submit');
                            if (resp == 'success') {
                                $('#tbxForgotEmailAddress').val('');
                                //$('#lnkThankyou').click()[0];
                                DisplMsg(divMsg, 'Temporary Password has been sent to registered Email Address.', 'alert-message success');
                            }
                            else if (resp == 'invalid') {
                                DisplMsg(divMsg, 'Entered email address is not registered.', 'alert-message error');
                            }
                            else if (resp == 'google') {
                                DisplMsg(divMsg, 'Entered email address is registered using google sign up. Please use Sign with Google option.', 'alert-message error');
                            }
                        }

                    });
                }
                else {
                    $('.divForgotFormGroup').addClass('has-error');
                }
            }
        }

        function CheckControlStatus() {
            if (jQuery.trim($('#tbxForgotEmailAddress').val()) != '') {
                $('.spnForgotEmail').hide();
                var tbxEmail = $('.divForgotFormGroup .help-block').not('.spnForgotEmail').html();
                if (tbxEmail != 'undefined' && tbxEmail != '') {
                    $('.divForgotFormGroup').addClass('has-error');
                }
            }
        }
        function HideShowSignup(type) {
            if (type == 'L') {
                $('#divLocalSignUp').show();
                $('#divGoogleSignup').hide();
                $('.signupHeading').text('Sign up with Local Account');
            }
            else {
                $('#divLocalSignUp').hide();
                $('#divGoogleSignup').show();
                $('.signupHeading').text('Sign up with Google');
            }
        }
    </script>

</body>
</html>
