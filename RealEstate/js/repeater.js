function BindCtrl()
{
    $('.step').click(function(){page=parseInt($(this).attr('name'));$('#btnPageClick').click();});
    $('th[column]').each(
        function(index)
        {  
           $(this).css('cursor','pointer');                                      
           $(this).click(function(){                                             
               SortColumn=$(this).attr('column');                
               if($(this).attr('class')=="ASC" || $(this).attr('class')=='')
                    SortType='DESC';
               else                              
                    SortType='ASC';                                                  
               $(this).attr('class',SortType);
               $('#btnPageClick').click();
           });
        }
    );    
    $('div[column]').each(
        function(index)
        {  
           $(this).css('cursor','pointer');                                      
           $(this).click(function(){                                             
               SortColumn=$(this).attr('column');                                              
               if($(this).attr('class')=="ASC" || $(this).attr('class')=='')
                    SortType='DESC';
               else                                             
                    SortType='ASC';                                                                      
               $(this).attr('class',SortType);
               $('#btnPageClick').click();
           });
        }
    );
    
    GetSelRecord();
    SetSortCol();
    $('.ASC').attr('title','Click here for descending');
    $('.DESC').attr('title','Click here for ascending');
    BindDrop();   
    $("#StrRecPerPage").html($('#hdnRecPerPage').attr('value'));
} 
function GetSelRecord()
{
    var total=0;
    var selID='0';
    $("input[type='checkbox']").each(
        function(index)
        {
            if ($(this).attr('id') != 'cbxAll')
            {
                if($(this).attr('checked')==true)
                {
                    selID=selID + ',' + $(this).attr('name');
                    total=total+1;
                }
            }
        }
     );
     $('#hdnID').attr('value',selID);
     $('#divRecSelect').html(total + ' Records selected');   
}  
function SetCbxBox(ctrl)
{        
    if(ctrl.checked==false)
    {
        $('#cbxAll').attr('checked',false);
    }
    else
    {
        var cnt=0;            
        $("input[type='checkbox']").each(
                function(index)
                {
                    if($(this).attr('id')!='cbxAll')
                    {
                        if($(this).attr('checked')==false)
                        {
                            cnt=1;
                        }                           
                    }
                 });
      $('#cbxAll').attr('checked',cnt==0 ?true : false);
     }    
     GetSelRecord();         
}     
function CbxAll(ctrl){$('input[type=\'checkbox\']').attr('checked',ctrl.checked);}
function ShowSearch(){
	if($('#divSearch').attr('class')=='hide-search'){
		$('#divSearch').attr('class','show-search');
		$('#divSearch').html('Show Search');
		$('#trSearch').slideUp('slow');
		ClearControls();
	}
	else{
		$('#divSearch').attr('class','hide-search');
		$('#divSearch').html('Hide Search');
		$('#trSearch').slideDown('slow');
	}
}
function SubmitForm()
{    
    
    var tmpstr = '';
    $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
   
    if (PageUrl.indexOf('?') == -1)
        tmpstr = '?';
    else
        tmpstr = '&';

    $.ajax(
    {        
        url: PageUrl + tmpstr + 'sorttype='+ SortType +'&sortcol=' + SortColumn + '&page=' + page,data:$('#'+FormName).serialize(),type:'post',
        success:function(response){$('#'+ RspCtrl).html(response);$.hideprogress();}
    });
    
    return false;
}
function Operation(ctrlMsg, type) {
    if ($('#hdnID').attr('value') == '0' || $('#hdnID').attr('value').length == 0) {
        DisplMsg(ctrlMsg, 'Please select at least one record.', 'alert-message error');
    }
    else {
        var typename = '';
        if (type == 'remove') {
            typename = "delete";
        }
        else if (type == 'active') {
            typename = "activate";
        }
        else if (type == 'inactive') {
            typename = "deactivate";
        }
        var messagetitle = '';
        messagetitle = "Are you sure you want to " + typename + " selected record(s)?"

        if (confirm(messagetitle)) {
            DisplMsg(ctrlMsg, '', '');
            $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            if (PageUrl.indexOf('?') == -1)
                PageUrl = PageUrl + '?';
            else
                PageUrl = PageUrl + '&';
            $.ajax(
                {
                    url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
                    success: function(response) { $('#' + RspCtrl).html(response); $.hideprogress(); }
                });
        }
    }
}

function SetSortCol()
{
    $('th[column]').each(
        function(index){
            if($(this).attr('column')==SortColumn)
            {
                $(this).attr('class',SortType);
            }
            else
            {
                $(this).attr('class','ASC');
            }
        }
    );
      $('div[column]').each(
        function(index){
            if($(this).attr('column')==SortColumn)
            {
                $(this).attr('class',SortType);
            }
            else
            {
                $(this).attr('class','ASC');
            }
        }
    );
}
$(document).ready(
    function(){BindCtrl();}
);    
function BindDrop()
{
    $('.myrs-pagination-maxvalue').show();
	$('.myrs-pagination-maxvalue-toggler').click(function() {$('.myrs-pagination-maxvalue-list').fadeIn('fast');return false;});
	$('.myrs-pagination-maxvalue').hoverIntent(function() {return false;}, function() {$('.myrs-pagination-maxvalue-list').fadeOut('fast');});        
	$('div:.myrs-pagination-maxvalue-list  ul li a').click(function(evt){	    
	   $('#hdnRecPerPage').attr('value',parseInt($(this).children().html()));
	   $('#btnPageClick').click();
	});
}
function ValidateCtrl()
{
   
    var i=0;
    var cnt=0;
    var strMsg='';    
    var ctrlArr=SearchControl.split('@');
    for(i=0;i<ctrlArr.length;i++)
    {
  
        var ctrl=ctrlArr[i].split(':');
        if($('#' + ctrl[0]).attr('value')==ctrl[1])
        {  
//            strMsg=strMsg + '<br/> - ' + ctrl[2];
            cnt++;
        }
    }    
    if(cnt ==ctrlArr.length)
    {        
        DisplMsg(divMsg,'Please search with at least one criteria.','alert-message error');        
        return false;
    }    
}
function ClearControls()
{
    var i=0;
    var strMsg='';    
    var ctrlArr=SearchControl.split('@');
    for(i=0;i<ctrlArr.length;i++)
    {
        var ctrl=ctrlArr[i].split(':');
        $('#' + ctrl[0]).attr('value',ctrl[1]);
    }        
}
