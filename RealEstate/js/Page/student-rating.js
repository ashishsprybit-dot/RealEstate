$(window).load(function () {
    GetStudentCurrentCategoryScoreAndPercentage(1);
    $('.myTable').each(function () {
        var myTable = $(this).DataTable({
            dom: "<'text-muted'Bi>\n        <'table-responsive'tr>\n        <'mt-4'p>",
            buttons: [],
            language: {
                paginate: {
                    previous: '<i class="fa fa-lg fa-angle-left"></i>',
                    next: '<i class="fa fa-lg fa-angle-right"></i>'
                }
            },
            bFilter: true,
            data: userdata,
            autoWidth: false,
            deferRender: true,
            pageLength: 100000,
            bPaginate: false
        });
    });


    $('.table-search').each(function () {
        var ID = jQuery.trim($(this).attr('rel'));
        var myTable = $('#myTable' + ID).DataTable();
        $(this).on('keyup change focus', function (e) {
            var filterBy = $('#filterBy' + ID).val();
            var hasFilter = filterBy !== '';
            var value = $('#table-search' + ID).val();

            myTable.search('').columns().search('').draw();
            if (hasFilter) {
                myTable.columns(filterBy).search(value).draw();
            } else {
                myTable.search(value).draw();
            }
        });
    });


    $('.filterBy').each(function () {
        var ID = jQuery.trim($(this).attr('rel'));
        var myTable = $('#myTable' + ID).DataTable();
        $(this).on('keyup change focus', function (e) {
            var filterBy = $('#filterBy' + ID).val();
            var hasFilter = filterBy !== '';
            var value = $('#table-search' + ID).val();

            myTable.search('').columns().search('').draw();
            if (hasFilter) {
                myTable.columns(filterBy).search(value).draw();
            } else {
                myTable.search(value).draw();
            }
        });
    });

});

function DescriptionChange(ele, no, QuestionID, StudentID) {
    var DisableStatus = $(ele).attr('disabled');
    if (DisableStatus == undefined || DisableStatus == false);
    {
        SaveRating(no, QuestionID, StudentID, 1);
    }
}
function ChangeNeverDemonstrate(ele, no, QuestionID, StudentID) {
    if ($(ele).prop('checked')) {
        $('.btnNeverDemonstrateYes').attr('onclick', "ConfirmNeverDemonstrateYes('" + no + "','" + QuestionID + "', '" + StudentID + "')");
        $('.btnNeverDemonstrateNo').attr('onclick', "ConfirmNeverDemonstrateNo('" + no + "','" + QuestionID + "', '" + StudentID + "')");
        $('.btnModel').click();
    }
    else {

        $('#ckb' + QuestionID + '1').attr('disabled', false);
        $('#ckb' + QuestionID + '2').attr('disabled', false);
        $('#ckb' + QuestionID + '3').attr('disabled', false);
        SaveRating(no, QuestionID, StudentID, -2);
    }
}
function ChangeNeverDemonstrateV2(ele, no, QuestionSubheadingSkillID, StudentID) {
    //$('.close').click();
    if ($(ele).prop('checked')) {
        $('#ckb' + QuestionSubheadingSkillID + '1').prop('checked', false);
        $('.btnNeverDemonstrateYes').attr('onclick', "ConfirmNeverDemonstrateYesV2('" + no + "','" + QuestionSubheadingSkillID + "', '" + StudentID + "')");
        $('.btnNeverDemonstrateNo').attr('onclick', "ConfirmNeverDemonstrateNoV2('" + no + "','" + QuestionSubheadingSkillID + "', '" + StudentID + "')");
        $('.btnModel').click();
        //$('.close').click();
    }
    else {
        $('#ckb' + QuestionSubheadingSkillID + '1').prop('checked', false);
        $('.btnNeverDemonstrateCancelYes').attr('onclick', "ConfirmNeverDemonstrateCancelYesV2('" + no + "','" + QuestionSubheadingSkillID + "', '" + StudentID + "')");
        $('.btnNeverDemonstrateCancelNo').attr('onclick', "ConfirmNeverDemonstrateCancelNoV2('" + no + "','" + QuestionSubheadingSkillID + "', '" + StudentID + "')");
        $('.btnNeverDemonstrateCancelModel').click();


        //$('#ckb' + QuestionSubheadingSkillID + '1').removeAttr('disabled');
        //$('#ckb' + QuestionSubheadingSkillID + '2').removeAttr('disabled');
        //$('#ckb' + QuestionSubheadingSkillID + '3').removeAttr('disabled');
             
        //SaveRating(no, QuestionSubheadingSkillID, StudentID, -2);
    }
}



function ConfirmNeverDemonstrateCancelNoV2(no, QuestionSubheadingSkillID, StudentID) {
      $('#ckbNeverDemonstrate' + QuestionSubheadingSkillID).prop('checked', true);
}

function ConfirmNeverDemonstrateCancelYesV2(no, QuestionSubheadingSkillID, StudentID) {
    $('.btnNeverDemonstrateCancelNo').removeAttr('onclick');
    $('.btnNeverDemonstrateCancelNo').click();

    $('#ckb' + QuestionSubheadingSkillID + '1').removeAttr('disabled');
    $('#ckb' + QuestionSubheadingSkillID + '2').removeAttr('disabled');
    $('#ckb' + QuestionSubheadingSkillID + '3').removeAttr('disabled');

    SaveRating(no, QuestionSubheadingSkillID, StudentID, -2);    
   
}





function ConfirmNeverDemonstrateNo(no, QuestionID, StudentID) {
    $('#ckbNeverDemonstrate' + QuestionID).prop('checked', false);
}
function ConfirmNeverDemonstrateYes(no, QuestionID, StudentID) {
    $('.btnNeverDemonstrateNo').removeAttr('onclick');
    $('.btnNeverDemonstrateNo').click();
    $('#tbx' + QuestionID + 'Date1').attr('disabled', true);
    $('#tbx' + QuestionID + 'Desc1').attr('disabled', true);
    $('#tbx' + QuestionID + 'Date2').attr('disabled', true);
    $('#tbx' + QuestionID + 'Desc2').attr('disabled', true);
    $('#tbx' + QuestionID + 'Date3').attr('disabled', true);
    $('#tbx' + QuestionID + 'Desc3').attr('disabled', true);

    $('#tbx' + QuestionID + 'Date1').val('');
    $('#tbx' + QuestionID + 'Desc1').val('');
    $('#tbx' + QuestionID + 'Date2').val('');
    $('#tbx' + QuestionID + 'Desc2').val('');
    $('#tbx' + QuestionID + 'Date3').val('');
    $('#tbx' + QuestionID + 'Desc3').val('');

    $('#ckb' + QuestionID + '1').prop('checked', false);
    $('#ckb' + QuestionID + '2').prop('checked', false);
    $('#ckb' + QuestionID + '3').prop('checked', false);


    $('#ckb' + QuestionID + '1').attr('disabled', true);
    $('#ckb' + QuestionID + '2').attr('disabled', true);
    $('#ckb' + QuestionID + '3').attr('disabled', true);
    SaveRating(no, QuestionID, StudentID, -1);
}

function ConfirmNeverDemonstrateNoV2(no, QuestionSubheadingSkillID, StudentID) {
    $('#ckbNeverDemonstrate' + QuestionSubheadingSkillID).prop('checked', false);
}
function ConfirmNeverDemonstrateYesV2(no, QuestionSubheadingSkillID, StudentID) {
    $('.btnNeverDemonstrateNo').removeAttr('onclick');
    $('.btnNeverDemonstrateNo').click();
    $('#tbx' + QuestionSubheadingSkillID + 'Date1').attr('disabled', true);
    $('#tbx' + QuestionSubheadingSkillID + 'Desc1').attr('disabled', true);
    $('#tbx' + QuestionSubheadingSkillID + 'Date2').attr('disabled', true);
    $('#tbx' + QuestionSubheadingSkillID + 'Desc2').attr('disabled', true);
    $('#tbx' + QuestionSubheadingSkillID + 'Date3').attr('disabled', true);
    $('#tbx' + QuestionSubheadingSkillID + 'Desc3').attr('disabled', true);

    $('#tbx' + QuestionSubheadingSkillID + 'Date1').val('');
    $('#tbx' + QuestionSubheadingSkillID + 'Desc1').val('');
    $('#tbx' + QuestionSubheadingSkillID + 'Date2').val('');
    $('#tbx' + QuestionSubheadingSkillID + 'Desc2').val('');
    $('#tbx' + QuestionSubheadingSkillID + 'Date3').val('');
    $('#tbx' + QuestionSubheadingSkillID + 'Desc3').val('');

    $('#ckb' + QuestionSubheadingSkillID + '1').prop('checked', false);
    $('#ckb' + QuestionSubheadingSkillID + '2').prop('checked', false);
    $('#ckb' + QuestionSubheadingSkillID + '3').prop('checked', false);


    $('#ckb' + QuestionSubheadingSkillID + '1').attr('disabled', true);
    $('#ckb' + QuestionSubheadingSkillID + '2').attr('disabled', true);
    $('#ckb' + QuestionSubheadingSkillID + '3').attr('disabled', true);
    SaveRating(no, QuestionSubheadingSkillID, StudentID, -1);
}
// Complete Level
function CompleteLevel(ele, CategoryID, StudentID) {
    //  if ($(ele).prop('checked')) {


    if (parseFloat($('.hdnCurrentLevelPercentage').val()) < parseFloat($('.hdnNextLevelThreshould').val())) {
        $('.MoveNextLevelMessage').html(' <h6>Are you absolutely sure you want to continue? </h6>Students should not be moved to the next level until at least <span class="badge badge-warning">' + $('.hdnNextLevelThreshould').val() + '%</span> of the current level has been completed. This student has only completed <span class="badge badge-warning">' + $('.hdnCurrentLevelPercentage').val() + '%</span> of this level.');
    }
    else {
        $('.MoveNextLevelMessage').html('<h6>Are you absolutely sure you want to continue?</h6>');
    }
    $('.btnCompleteLevelYes').attr('onclick', "ConfirmCompleteLevelYes('" + ele + "','" + CategoryID + "', '" + StudentID + "')");
    $('.btnCompleteLevelNo').attr('onclick', "ConfirmCompleteLevelNo('" + ele + "','" + CategoryID + "', '" + StudentID + "')");
    $('.btnCompleteLevelModel').click();
    //}
    //else {
    //    CompleteLevelSave(ele, CategoryID, StudentID);            
    //}
}
function ConfirmCompleteLevelYes(ele, CategoryID, StudentID) {
    $('.btnCompleteLevelNo').removeAttr('onclick');
    $('.btnCompleteLevelNo').click();

    CompleteLevelSave(ele, CategoryID, StudentID);
}
function ConfirmCompleteLevelNo(ele, CategoryID, StudentID) {
    $('#ckbLevelCompleted' + CategoryID).prop('checked', false);
}