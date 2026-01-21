<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="category-list.aspx.cs" Inherits="AdminPanel_Category_List" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>

<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <div class="d-flex justify-content-between">
            <h1 class="page-title">Category List</h1>
            <div class="btn-toolbar">
                <button class="btn btn-outline-primary"
                    data-toggle="modal"
                    data-target="#ImportExcel"
                    style="margin-right: 30px; <%: CLT_Version != 2 ? "display:none;" : "" %>">
                    <i class="fas fa-cloud-upload-alt"></i>&nbsp;Bulk Upload
                </button>

                 <button type="button" style="margin-right: 20px;" class="bmd-modalButton btn btn-warning btnChangeOrder" data-toggle="modal" data-bmdsrc="<%=Utility.Config.WebSiteUrl %>/adminpanel/category-order.aspx" data-bmdwidth="1000" data-bmdheight="900" data-target="#myModal" data-bmdvideofullscreen="true">Change Category Order</button>
               

                <button type="button" class="btn btn-primary" onclick="window.location.href='category-modify.aspx'">Add Category</button>

                

            </div>
        </div>
        <%--<p class="text-muted">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>--%>
    </header>

    <!-- /.page-title-bar -->
    <div class="alert alert-danger hide" id="divMsg" runat="server" style="display: none;"></div>

    <div class="page-section">
        <!-- .card -->
        <div class="card card-fluid">
            <!-- .card-body -->
            <div class="card-body">
                <!-- .form-group -->
                <div class="form-group">
                    <!-- .input-group -->
                    <div class="input-group input-group-alt">
                        <!-- .input-group-prepend -->
                        <div class="input-group-prepend">
                            <select id="filterBy" class="custom-select">
                                <option value='' selected>Filter By </option>
                                <option value="0">Name</option>
                            </select>
                        </div>
                        <!-- /.input-group-prepend -->
                        <!-- .input-group -->
                        <div class="input-group has-clearable">
                            <button id="clear-search" type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><span class="oi oi-magnifying-glass"></span></span>
                            </div>
                            <input id="table-search" type="text" class="form-control" placeholder="Search Category">
                        </div>
                        <!-- /.input-group -->
                    </div>
                    <!-- /.input-group -->
                </div>
                <!-- /.form-group -->
                <form id="frmSearch" action="employee-list.aspx">
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnSearch" name="hdnSearch" />
                </form>
                <!-- .table -->
                <table id="myTable" class="table">
                    <!-- thead -->
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Layout</th>
                            <th width="100" class="text-center">Questions</th>
                            <th width="70" class="text-center">Status </th>
                            <th width="70" class="text-center">Edit </th>
                            <th width="70" class="text-center">Delete </th>
                        </tr>
                    </thead>
                    <!-- /thead -->
                    <!-- tbody -->
                    <tbody>
                        <!-- create empty row to passing html validator -->
                    </tbody>
                    <!-- /tbody -->
                </table>
                <!-- /.table -->
            </div>
            <!-- /.card-body -->
        </div>
        <!-- /.card -->
    </div>


    <!-- Bootstrap 5 Modal -->
    <div class="modal fade" id="ImportExcel" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title" id="modalTitle">
                        <i class="fas fa-file-upload"></i>&nbsp;Bulk Upload Questions
                    </h5>


                    <button type="button" onclick="closeModal()" class="close" data-dismiss="modal" aria-label="Close" style="color: white;">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>

                <div class="modal-body  text-center p-4">
                    <div id="ImpMessage">
                    </div>


                    <!-- Drag & Drop Area -->
                    <div id="dropArea" class="border border-primary rounded p-4 text-muted"
                        ondragover="event.preventDefault()"
                        ondrop="handleDrop(event)">
                        <i class="fas fa-cloud-upload-alt fa-3x text-primary"></i>
                        <p class="mt-2">Drag & Drop your Excel file here</p>
                        <p>or</p>

                        <!-- Hidden File Input with Custom Button -->
                        <label class="btn btn-outline-primary">
                            <i class="fas fa-folder-open"></i>&nbsp;Browse File
                        <input type="file" id="fileUpload" class="d-none" accept=".xlsx, .xls" onchange="updateFileName()">
                        </label>

                        <!-- Display File Name -->
                        <p id="fileName" class="mt-2"></p>
                    </div>
                </div>

                <div class="modal-footer d-flex justify-content-between">
                    <button id="btnDownload" class="btn  btn-warning ms-auto"
                        data-toggle="modal"
                        style="color: #363642;">
                        <i class="fa fa-file-excel"></i>&nbsp;Download Format 
                    </button>



                    <button type="button" class="btn btn-secondary ml-auto" data-dismiss="modal">
                        Close               
                    </button>
                    <button type="button" class="btn btn-primary" onclick="UploadExcel()">
                        <i class="fas fa-upload"></i>&nbsp;Upload
                    </button>
                </div>
            </div>
        </div>
    </div>

      <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content bmd-modalContent" style="min-height:550px;">

                <div class="modal-body">

                    <div class="close-button">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="embed-responsive embed-responsive-16by9" style="min-height:510px;">
                        <iframe class="embed-responsive-item" frameborder="0"></iframe>
                    </div>
                </div>
                 
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Font Awesome for Icons -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/js/all.min.js"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'category-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'tbxFname::FirstName@tbxLname::LastName@tbxUname::Username@tbxEmail::EmailId';
        var userdata = <%= data %>;

        

        function OpenAccordian(ele, ID) {
            if ($(ele).text() == '+') {
                $(ele).text('-');
                $('.cls' + ID).removeClass('hide');
            }
            else {
                $(ele).text('+');
                $('.cls' + ID).addClass('hide');
                if ($('.mainid' + ID).hasClass('MainParent')) {
                    HideAllChilds(ID);
                }
            }
        }

        function HideAllChilds(ID) {
            $('.cls' + ID).each(function () {
                $(this).addClass('hide');
                var ChildID = $(this).attr('rel');
                $('.lnk' + ChildID).text('+');
                $('.cls' + ChildID).each(function () {
                    $(this).addClass('hide');
                });


            });

        }
    </script>
    <!-- BEGIN PAGE LEVEL JS -->
    <script src="assets_1/javascript/pages/dataTables.bootstrap.js"></script>
    <script src="js/Page/datatables-category.js"></script>

    <script type="text/javascript">
         

        $(window).load(function () {
            BindTotal();

            $("#btnDownload").click(function () {
                window.location.href = '<%=Utility.Config.WebSiteUrl %>adminpanel/assets/uploadify/Upload.ashx?method=DownloadExcelFormat';
            });
        })
        function BindTotal() {
            $('.MainParent').each(function () {
                var SUM = 0
                var ChildSUM = 0
                var ChildID = $(this).attr('rel');
                $('.cls' + ChildID).each(function () {
                    var ChildChildID = $(this).attr('rel');
                    ChildID = 0;
                    $('.cls' + ChildChildID).each(function () {

                        SUM = SUM + parseInt($(this).find('.qcount').text());
                        ChildID = ChildID + parseInt($(this).find('.qcount').text());

                    });
                    $(this).find('.spnqcount').text(ChildID);
                    $(this).find('.spnqcount').addClass('badge badge-subtle1 badge-success');
                });
                $(this).find('.spnqcount').text(SUM);
                $(this).find('.spnqcount').addClass('badge badge-warning');


            });

        }



        function updateFileName() {
            var fileInput = document.getElementById("fileUpload");
            var fileNameDisplay = document.getElementById("fileName");

            if (fileInput.files.length > 0) {
                fileNameDisplay.textContent = "Selected File: " + fileInput.files[0].name;
            } else {
                fileNameDisplay.textContent = "";
            }
        }

        function handleDrop(event) {
            event.preventDefault();
            var fileInput = document.getElementById("fileUpload");
            var fileNameDisplay = document.getElementById("fileName");

            if (event.dataTransfer.files.length > 0) {
                fileInput.files = event.dataTransfer.files;
                fileNameDisplay.textContent = "Selected File: " + event.dataTransfer.files[0].name;
            }
        }


        function closeModal() {
            var msgDiv = document.getElementById('ImpMessage');

            $("#ImportExcel").modal("hide");
            setTimeout(() => {
                msgDiv.style.display = "none";
            }, 100);

        }



        function displayFileName() {
            var fileUpload = document.getElementById('fileUpload');
            var fileNameText = document.getElementById('fileNameText');

            if (fileUpload.files.length > 0) {
                fileNameText.textContent = "Selected file: " + fileUpload.files[0].name;
            } else {
                fileNameText.textContent = "Click to Upload";
            }
        }

        function UploadExcel() {
            var fileUpload = $("#fileUpload")[0].files[0];

            if (!fileUpload) {
                // showMessage("Please select a file.", "danger");

                alert("Please select a file.")

                return;
            }

            // Validate file type (only Excel files allowed)
            var fileExtension = fileUpload.name.split('.').pop().toLowerCase();
            var allowedExtensions = ['xlsx', 'xls'];
            if (!allowedExtensions.includes(fileExtension)) {
                // showMessage("Invalid file type. Please upload an Excel file.", "danger");
                alert("Invalid file type. Please upload an Excel file.")

                return;
            }

            // var CategoryID = getQueryStringParameter('cid'); // Get CategoryID from URL
            var formData = new FormData();
            formData.append("file", fileUpload);
            // formData.append("CategoryID", CategoryID);

            $.ajax({
                type: "POST",
                url: '<%=Utility.Config.WebSiteUrl %>adminpanel/assets/uploadify/Upload.ashx?method=UploadExcelFile',
                data: formData,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    showMessage("Uploading file...", "info");
                },
                success: function (response) {
                    try {
                        var res = JSON.parse(response);

                        if (res.message.includes("Duplicate data found") && res.filePath.includes("/ErrorFiles/")) {
                            var filePath = res.filePath.match(/\/source\/ErrorFiles\/[^\"]+/);
                            if (filePath && filePath.length > 0) {
                                var fileUrl = encodeURIComponent(filePath[0]);

                                showMessage("Error in file. Please check downloaded file.", "danger", 5000);

                                //setTimeout(() => $("#ImportExcel").modal("hide"), 500);
                                $('#fileName').text('');
                                $('#fileUpload').val('');

                                // Redirect to download the error file
                                window.location.href = '<%= Utility.Config.WebSiteUrl %>adminpanel/assets/uploadify/Upload.ashx?method=DownloadErrorFile&filePath=' + fileUrl;
                            } else {
                                console.error("Error: Could not extract file path from response.");
                                showMessage("Unexpected error. Please try again.", "danger");
                            }
                        }
                        else if (res.message.includes("Invalid column format found in uploaded sheet")) {
                            //  setTimeout(() => $("#ImportExcel").modal("hide"), 500);
                            $('#fileName').text('');
                            $('#fileUpload').val('');

                            showMessage(res.message, "danger", 3000);

                        }

                        else if (res.message.includes("Subject cannot be null or empty.")) {

                            $('#fileName').text('');
                            $('#fileUpload').val('');
                            showMessage(res.message, "danger", 3000);

                        }
                        else if (res.message.includes("Level cannot be null or empty.")) {

                            $('#fileName').text('');
                            $('#fileUpload').val('');
                            showMessage(res.message, "danger", 3000);

                        }
                        else if (res.message.includes("Only Skill1 to Skill15 are allowed. Remove extra skill columns.")) {

                            $('#fileName').text('');
                            $('#fileUpload').val('');
                            showMessage(res.message, "danger", 3000);

                        }
                        else if (res.message.includes("Strand cannot be null or empty.")) {

                            $('#fileName').text('');
                            $('#fileUpload').val('');
                            showMessage(res.message, "danger", 3000);

                        }
                        else if (res.message.includes("Data imported successfully")) {
                            //setTimeout(() => $("#ImportExcel").modal("hide"), 2000);
                            setTimeout(() => window.location.href = 'category-list.aspx');

                            showMessage("File uploaded successfully.", "success", 2000);

                            $('#fileName').text('');
                            $('#fileUpload').val('');

                            // Hide modal and redirect after success
                        }
                        else {
                              $('#fileName').text('');
                            $('#fileUpload').val('');
                            showMessage(res.message, "danger", 3000);
                        }
                    } catch (e) {
                        console.error("Invalid JSON response:", response);
                        showMessage("Unexpected response format.", "danger");
                    }
                },
                error: function (xhr) {
                    console.error("Error:", xhr.responseText);
                    showMessage("Error uploading file. Please try again.", "danger");
                }
            });
        }





        //  Function to Show Messages
        function showMessage(message, type) {
            var msgDiv = document.getElementById('ImpMessage');
            if (!msgDiv) return;

            msgDiv.innerHTML = message;
            msgDiv.className = `alert alert-${type}`;
            msgDiv.style.display = "block";


            if (type === "success") {
                setTimeout(() => {
                    msgDiv.style.display = "none";
                }, 8000);
            }
            else {
                setTimeout(() => {
                    msgDiv.style.display = "none";
                }, 12000);
            }
        }


        // Function to get query string parameters
        function getQueryStringParameter(name) {
            var params = new URLSearchParams(window.location.search);
            return params.get(name);
        }



    </script>
    <script>
         (function ($) {
              
            $.fn.bmdIframe = function (options) {
                var self = this;
                var settings = $.extend({
                    classBtn: '.bmd-modalButton',
                    defaultW: 1000,
                    defaultH: 700
                }, options);
                console.log(settings)
                $(settings.classBtn).on('click', function (e) {
                    var allowFullscreen = $(this).attr('data-bmdVideoFullscreen') || false;
                    debugger;
                    var dataVideo = {
                        'src': $(this).attr('data-bmdSrc'),
                        'height': $(this).attr('data-bmdHeight') || settings.defaultH,
                        'width': $(this).attr('data-bmdWidth') || settings.defaultW
                    };

                    if (allowFullscreen) dataVideo.allowfullscreen = "";

                    // stampiamo i nostri dati nell'iframe
                    $(self).find("iframe").attr(dataVideo);
                });

                // se si chiude la modale resettiamo i dati dell'iframe per impedire ad un video di continuare a riprodursi anche quando la modale è chiusa
                this.on('hidden.bs.modal', function () {
                    $(this).find('iframe').html("").attr("src", "");
                });

                return this;
            };

        })(jQuery);

        $(document).ready(function () {
             jQuery("#myModal").bmdIframe();
        });
    </script>
</asp:Content>
