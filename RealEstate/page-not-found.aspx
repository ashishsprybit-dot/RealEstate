<%@ Page Language="C#" AutoEventWireup="true" CodeFile="page-not-found.aspx.cs" Inherits="page_not_found" %>
<%@Import Namespace="Utility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    
     <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">    
    <title><%= new BAL.GeneralSettings().getConfigValue("WebsiteName").ToString()%></title>    
    <link href="https://fonts.googleapis.com/css?family=Fira+Sans:400,500,600" rel="stylesheet">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/vendor/open-iconic/font/css/open-iconic-bootstrap.min.css">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/vendor/%40fortawesome/fontawesome-free/css/all.min.css">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/vendor/flatpickr/flatpickr.min.css">
    <!-- END PLUGINS STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/stylesheets/theme.min.css" data-skin="default">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/stylesheets/custom.css">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>validation-lib/dev.css" />    
    <%--<link href="css/loadingbox.css" rel="stylesheet" />--%>
    <link href="<%=Config.VirtualDir %>style/dev.css" rel="stylesheet" />
    <%--<script src="assets/js/repeater.js"></script>--%>
      <script src="<%=Config.VirtualDir %>js/jquery-1.10.2.min.js"></script>
</head>
<body>
   <main class="empty-state empty-state-fullpage bg-black">
      <!-- .empty-state-container -->
      <div class="empty-state-container">
        <div class="card">
          <div class="card-header bg-light text-left">
            <i class="fa fa-fw fa-circle text-red"></i> <i class="fa fa-fw fa-circle text-yellow"></i> <i class="fa fa-fw fa-circle text-teal"></i>
          </div>
          <div class="card-body">
            <div class="state-figure">
              <img class="img-fluid" src="assets/images/illustration/img-2.svg" alt="" style="max-width: 320px">
            </div>
            <h3 class="state-header"> Page not found! </h3>
            <p class="state-description lead"> This might be because: </p>
              <p>
                  You may have typed the web address incorrectly. Please check the address and spelling ensuring that it does not contain spaces.
              </p>
            <div class="state-action">
              
            </div>
          </div>
        </div>
      </div><!-- /.empty-state-container -->
    </main>
</body>
</html>
