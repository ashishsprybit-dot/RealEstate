<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title">Hi <%=Session["TeacherName"] %> </h1>
        <div id="divAdmin" runat="server" visible="false">

            <p class="text-muted">Manage your users, home groups, students and more.</p>

            <div class="row teacherdashboard">
                <%--  <div class="masonry-item col-lg-3">
                    <!-- .card -->
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="media align-items-center">
                                <div class="col-auto">
                                    <a class="user-avatar user-avatar-xl dashboard-icon">
                                        <img src="<%=Config.VirtualDir %>images/icons/dashboard_user_icon.svg" alt=""></a>
                                </div>
                                <div class="col">
                                    <span>Users</span>
                                    <h2><%=Teachers %></h2>
                                </div>
                            </div>                           
                        </div>                        
                    </div>
                </div>--%>
                <div class="masonry-item col-lg-3">
                    <!-- .card -->
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="media align-items-center">
                                <div class="col-auto">
                                    <a class="user-avatar user-avatar-xl dashboard-icon">
                                        <img src="<%=Config.VirtualDir %>images/icons/dashboard_home_group_icon.svg" alt=""></a>
                                </div>
                                <div class="col">
                                    <span>Home Groups</span>
                                    <h2><a href="<%=Config.WebSiteUrl + SchoolURL %>/home-group-list.aspx" class="clrblack"><%=HomeGroups %></a></h2>
                                </div>
                            </div>
                            <!-- /grid row -->
                        </div>
                        <!-- /.card-body -->
                    </div>
                </div>
                <div class="masonry-item col-lg-3">
                    <!-- .card -->
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="media align-items-center">
                                <div class="col-auto">
                                    <a class="user-avatar user-avatar-xl dashboard-icon">
                                        <img src="<%=Config.VirtualDir %>images/icons/dashboard_student_icon.svg" alt=""></a>
                                </div>
                                <div class="col">
                                    <span>My Students</span>
                                    <h2><a href="<%=Config.WebSiteUrl + SchoolURL %>/student-list.aspx" class="clrblack"><%=Students %></a></h2>
                                </div>
                            </div>
                            <!-- /grid row -->
                        </div>
                        <!-- /.card-body -->
                    </div>
                </div>
              <%--  <div class="masonry-item col-lg-3">
                    <!-- .card -->
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="media align-items-center">
                                <div class="col-auto">
                                    <a class="user-avatar user-avatar-xl dashboard-icon">
                                        <img src="<%=Config.VirtualDir %>images/icons/dashboard_student_icon.svg" alt=""></a>
                                </div>
                                <div class="col">
                                    <span style="display: block; width: 137px;">Assessment Pending</span>
                                    <h2 class="orange"><a href="<%=Config.WebSiteUrl + SchoolURL %>/home-group-list.aspx"><%=PendingStudents %></a></h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>

            <%--   <hr />--%>
        </div>
        <div class="d-flex flex-column flex-md-row">
            <p class="lead">
                When completing student judgements, consider the criteria and sources of evidence below and think about the various ways that students can demonstrate competency in each of the elaborations.            
            </p>
        </div>
        <br />
        <h4 class="mb-3">Curriculum Level Tracker Summary</h4>
    </header>

    <div class="page-section">
        <!-- .section-block -->
        <div class="section-block">
            <div class="metric-row">
                <div class="col-lg-12">
                    <div class="metric-row metric-flush dashboard">
                        <div class="col">
                            <div class="card card-fluid text-center1">

                                <div class="card-body teacherdashboard">
                                    <div>
                                        <p>
                                            The Curriculum Level Tracker (CLT) supports the collection of evidence for individual students in order for teachers to determine an accurate Victorian Curriculum Level. Three pieces of evidence, from formal or informal assessment sources, are required as evidence against each content descriptor. A date and description must be provided for each piece of evidence. Attachments (pictures, videos, documents etc.) may be provided to support the evidence. The tracker will calculate a percentage of the level completed. Students will remain in their current level until the teacher ticks the ‘Move to Next Level’ box at the bottom of the page. The teacher may do this once at least 80% of the current level they are working within has been achieved. 
                                        </p>

                                        <blockquote>
                                            <p>
                                                <b>NOTE:</b> Evidence can be documented in levels above the current level that a student is working within. For example, if a student has a <i>Score</i> of A.53 (working within Level A) and evidence is provided for one content descriptor within Level B, their <i>Score</i> will increase to somewhere around A.60 (this figure will vary depending on the amount of content descriptors in that particular area). Providing evidence, in any levels, will only increase the <i>Score</i> up to 99% (eg. A.99). Any evidence provided after this point will not be reflected in the <i>Score</i> until the teacher has checked the ‘Move Student to Next Level’ box.
                                            </p>
                                        </blockquote>

                                        <p>
                                            As the student’s <i>Score</i> can be changed by levels above their current working level, two graphs will be shown on their assessment page to outline the difference between their <i>Score</i> and their completion of the current level.
                                        </p>
                                        <p>
                                            1. The ‘<i>Completion of Level</i>’ Graph: Shows the percentage of completion for the level the student is currently working within. ie. 60% of Level C.<br />
                                            2. The ‘<i>Score</i>’ Graph: Shows the student’s calculated Victorian Curriculum score, based on evidence provided in the current level AND any levels above.

                                        </p>
                                        <p>
                                            <b>Not Priority Learning:</b> This option is only used to narrow the scope of learning for a student in order to focus attention on and or progress them along the sequence of their priority learning. Using this option removes that particular content descriptor from the curriculum level calculation. Relevant school leaders should be consulted before this option is used. 
                                        </p>
                                        <p>
                                            <b>Submitting Results:</b> Evidence can be collected and entered throughout the semester. At the end of the semester, the teacher needs to <i>Submit</i> the class results. The submission signifies that all necessary evidence has been provided for that semester and no further change will be made. A final submission date should be decided upon by the school towards the end of the semester, as the final Scores will be required for end of semester reporting. The CLT School Administrator should check that all classes have been submitted on the submission date.

                                        </p>
                                        <p>
                                            <b>Victorian Curriculum Reporting:</b> Although CLT scores will be more accurate in showing student growth, a ‘rounded’ Victorian Curriculum Score will need to be provided to the Department of Education at the end of each semester via your end of semester reporting. For this purpose, the score should always be rounded DOWN to the nearest possible Victorian Curriculum score. For example:
                    <br />
                                            <br />
                                            Levels A – C: .00 – .99 = Round down to zero. 
                    <br />
                                            eg. A.15 = A, A.99 = A<br />
                                            <br />
                                            Levels D+ :	.00 – .49 = Round down to zero	.50 - .99 = Round down to .5<br />
                                            eg. D.15 = D, D.99 = D.5<br />
                                            eg. 1.49 = 1, 1.80 = 1.5
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- metric row -->
            <%--<div class="metric-row">
                <div class="col-lg-12">
                    <div class="metric-row metric-flush dashboard">
                        <div class="col">
                            <div class="card card-fluid">
                                <div class="card-header"><i class="fa fa-ban text-info"></i></div>
                                <div class="card-body">
                                    The student is <span class="text-info">unable</span> to demonstrate competence in any of the elaborations.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card card-fluid">
                                <div class="card-header"><i class="fa fa-circle-notch text-muted-circle"></i></div>
                                <div class="card-body">
                                    The student has <span class="text-muted">not yet</span> attempted any of the elaborations.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card card-fluid">
                                <div class="card-header"><i class="fa fa-star text-danger"></i></div>
                                <div class="card-body">
                                    The student demonstrates competence in a <span class="text-danger">few</span> of the elaborations with a <span class="text-danger">high level</span> of verbal, visual or gestural prompting in <span class="text-danger">one</span> environment with <span class="text-danger">one</span> familiar adult.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card card-fluid">
                                <div class="card-header"><i class="fa fa-star text-warning"></i><i class="fa fa-star text-warning"></i></div>
                                <div class="card-body">
                                    The student demonstrates competence in <span class="text-warning">most</span> of the elaborations with a <span class="text-warning">moderate level</span> of verbal, visual or gestural prompting in <span class="text-warning">two or more</span> environment with <span class="text-warning">two</span> familiar adults.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card card-fluid">
                                <div class="card-header"><i class="fa fa-star text-success"></i><i class="fa fa-star text-success"></i><i class="fa fa-star text-success"></i></div>
                                <div class="card-body">
                                    The student demonstrates competence in <span class="text-success">all</span> of the elaborations with a <span class="text-success">little or no</span> verbal, visual or gestural prompting across <span class="text-success">many</span> environments with <span class="text-success">familiar and unfamiliar</span> adults.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <!-- metric column -->
            <h4 class="mb-3">Sources of Evidence</h4>
            <div class="metric-row">
                <div class="col-lg-12">
                    <div class="metric-row metric-flush">

                        <div class="col">
                            <!-- .metric -->
                            <div class="metric metric-bordered align-items-center1">
                                <ul class="list-unstyled1">
                                    <li>Informal observations</li>
                                    <li>Annotated work samples</li>
                                    <li>Photo evidence</li>
                                    <li>Video evidence</li>
                                    <li>Cross-checks</li>
                                    <li>Rubrics</li>
                                    <li>Student notes</li>
                                </ul>
                            </div>
                            <!-- /.metric -->
                        </div>

                        <div class="col">
                            <!-- .metric -->
                            <div class="metric metric-bordered align-items-center1">
                                <ul class="list-unstyled1">
                                    <li>ABLES</li>
                                    <li>Observation surveys</li>
                                    <li>Running records</li>
                                    <li>On-Demand assessments</li>
                                    <li>Maths Online Interviews</li>
                                    <li>English Online Interviews</li>
                                    <li>Oxford Numeracy Assessments</li>
                                </ul>
                            </div>
                            <!-- /.metric -->
                        </div>

                    </div>
                </div>
            </div>
            <%--<div class="metric-row">
                <div class="col-lg-12">
                    <div class="metric-row metric-flush dashboard">
                        <div class="col">
                            <div class="card card-fluid text-center1">

                                <div class="card-body">
                                    <ul class="list-unstyled1">
                                        <li>Informal observations</li>
                                        <li>Annotated work samples</li>
                                        <li>Photo evidence</li>
                                        <li>Video evidence</li>
                                        <li>Cross-checks</li>
                                        <li>Rubrics</li>
                                        <li>Student notes</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card card-fluid text-center1">

                                <div class="card-body">
                                    <ul class="list-unstyled1">
                                        <li>ABLES</li>
                                        <li>Observation surveys</li>
                                        <li>Running records</li>
                                        <li>On-Demand assessments</li>
                                        <li>Maths Online Interviews</li>
                                        <li>English Online Interviews</li>
                                        <li>Oxford Numeracy Assessments</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <!-- /metric column -->
        </div>
        <!-- /metric row -->
    </div>
    <!-- /.section-block -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
</asp:Content>

