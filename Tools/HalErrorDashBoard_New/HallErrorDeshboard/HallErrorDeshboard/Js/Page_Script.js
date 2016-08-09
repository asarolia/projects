function ValidateSearch()
{
    /*We can have another method to validate the corrent entries by getting the table Schema 
    and cheking if the respective colunm is present in the table Schema*/
    var txtRptNum = trimString(document.getElementById("txtRptNum").value);
    var txtPolNum = trimString(document.getElementById("txtPolNum").value);
    var txtPolNum = trimString(document.getElementById("txtPolNum").value);
    txtMsgId = trimString(document.getElementById("txtMsgId").value);
    if((document.getElementById("DropDownList2").value == "PRT_FAIL" || document.getElementById("DropDownList2").value == "CANX_FAIL" || document.getElementById("DropDownList2").value == "MISSING_AR" || document.getElementById("DropDownList2").value == "CANX_DONE_INTF_FAIL") && txtMsgId != "") 
    {
        alert("Report Selected in Dropdown does not have a Msg ID");
        return false;
    }
    if((txtRptNum != "" && txtPolNum != "") ||(txtMsgId != "" && txtRptNum != "")||(txtPolNum != "" && txtMsgId != ""))
        {
            alert("Enter only one field to proceed");
            return false;
        }     
    if(txtRptNum == "" && txtPolNum == "" && txtMsgId == "")
    {
        alert("Enter Reciept Number or Policy Number or Message ID  to proceed");
        return false;                                                               
    }
    if((document.getElementById("DropDownList2").value == "CANX_FAIL"||document.getElementById("DropDownList2").value == "PRT_FAIL" || document.getElementById("DropDownList2").value == "INTERFACE_FAIL") && txtRptNum != "")                                
    {
        alert("Report Selected in Dropdown dont have a Receipt Number");
        return false;
    }                
    return true;               
}
function ValidateDropdown()
    {
        if(document.getElementById("drdReport").value=="Select")
        {
            alert("Select a Report from Dropdown");
            return false;
        }
    }
function ValidateText()
{
        if(document.getElementById("txtLoginName").value == '' || document.getElementById("txtPass").value == '' || document.getElementById("txtLstNme").value == '' || document.getElementById("txtFstNme").value == '' || document.getElementById("txtEmail").value == '' || document.getElementById("txtLoginName").value == '')
        {
            alert("Enter Mandatory(*) fields");
            return false;
        }                                  
}
function ValidateDrop()
{
    if(document.getElementById("drdUserID").value=="Select")
     {
       alert("Select a User Name");                   
       return false;
     }  
}
function ValidateCol()
{
    var strValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
    var strValue= document.getElementById("txtColName").value;
    for (i = 0; i < strValue.length; i++)
    {
        strChar = strValue.charAt(i);
        if (strValidChars.indexOf(strChar) == -1)
            {
                 alert("Enter Only Characters");
                 document.getElementById("txtColName").focus();
                 return false;
            }
    }
    if(document.getElementById("txtColName").value=='')
    {
        alert("Enter Column Name");
        document.getElementById("txtColName").focus();
        return false;
    }
    if(document.getElementById("drdDataType").value != "datetime" && document.getElementById("drdDataType").value != "int")
    {
        if(document.getElementById("txtLength").value=='')
        {
        alert("Enter Length for the selected DataType");
        document.getElementById("txtLength").focus();
        return false;
        }
    }
                             
}
function isDate(source, dateValue)
{
	dateValue.IsValid = false;
    var value = trimString(dateValue.Value);
    var sourceControl = source.controltovalidate;
     if(value.length == 10)
		            {
			            //var patt=/([0-9][0-9].[0-9][0-9].[0-9][0-9][0-9][0-9])/g;
			            var patt=/([0-9]{2}.[0-9]{2}.[0-9]{4})/g;
			            var pattNext=/([0-9]{2}\/[0-9]{2}\/[0-9]{4})/g;
			            var result=patt.test(value);
			            if (result)
			            {
				            value = value.substring(6,10) + "-" + value.substring(3,5) + "-" + value.substring(0,2) ;				            
				            if(isValidDate(value))
				            {
				                document.getElementById(sourceControl).value = value;
					            dateValue.IsValid = true;
				            }
			            }
			            else if(pattNext.test(value))
			            {
			                value = value.substring(6,10) + "-" + value.substring(3,5) + "-" + value.substring(0,2) ;				            
				            if(isValidDate(value))
				            {
				                document.getElementById(sourceControl).value = value;
					            dateValue.IsValid = true;
				            }
			            }
			            else
			            {
				            //var nextpatt=/([0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9])/g;
				            var nextpatt=/([0-9]{4}-[0-9]{2}-[0-9]{2})/g;
				            var newresult = nextpatt.test(value);
				            if(newresult)
				            {
					            if(isValidDate(value))
						            {
							            dateValue.IsValid = true;
						            }
				            }
			            }
		            }
//        if(value.length == 8)
//            {
//                var patt=/([0-9]{8})/g;
//                var result=patt.test(value);
//                if(result)
//                {
//	                value = value.substring(0,4) + "-" + value.substring(4,6) + "-" + value.substring(6,8) ;					            
//	                if(isValidDate(value))
//		                {
//		                    document.getElementById(sourceControl).value = value;
//			                dateValue.IsValid = true;
//		                }
//                }
//            }
    }



function trimString(strInput)
{
	if (strInput == undefined) return "";
	var intLen = strInput.length;
	var intBegin = 0;
	var intEnd = intLen-1;
	var strRet = "";

	while (strInput.charAt(intBegin) == " "  &&  intBegin < intLen)
		intBegin++;

	while (strInput.charAt(intEnd) == " "  &&  intBegin < intEnd)
		intEnd--;

	strRet = strInput.substring(intBegin, intEnd+1);
	return strRet;
}


function isValidDate(str)
{
  if (str.length != 10) { return false }

  for (j=0; j<str.length; j++) 
	  {
		if ((j == 4) || (j == 7)) 
		{		
			  if (str.charAt(j) != "-") { return false }
	    } 

	}
  


  var day = str.charAt(8) == "0" ? parseInt(str.substring(9,10)) : parseInt(str.substring(8,10));
  var month = str.charAt(5) == "0" ? parseInt(str.substring(6,7)) : parseInt(str.substring(5,7));

  var begin = str.charAt(0) == "0" ? (str.charAt(1) == "0" ? (str.charAt(2) == "0" ? 3 : 2) : 1) : 0;
  var year = parseInt(str.substring(begin, 4));

  if (day == 0) { return false }
  if (month == 0 || month > 12) { return false }
  if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
	  {
	    if (day > 31) { return false }
	  }
	else 
		{
			if (month == 4 || month == 6 || month == 9 || month == 11) 
					{
						if (day > 30) { return false }
					} 
			else 
					{
						if (year%4 != 0) 
								{
									if (day > 28) { return false }
								} 
						else 
							{
								if (day > 29) { return false }
							}
					}
			}
  return true;
}
function ValidateAllOpen()
{
    var txtRptNum = trimString(document.getElementById("txtRptNum").value);
    var txtPolNum = trimString(document.getElementById("txtPolNum").value);    
    var txtMsgId = trimString(document.getElementById("txtMsgId").value);
    
    if(txtRptNum != "")
        {
            alert("Please Clear Receipt Number to use this button!!");
            return false;
        }
    if(txtPolNum != "")
        {
            alert("Please Clear Policy Number to use this button!!");
            return false;
        }
    if(txtMsgId != "")
        {
            alert("Please Clear Message ID to use this button!!");
            return false;
        }
        
            
    if(document.getElementById("DropDownList2").value == "All_the_Reports")
    {
        alert("Please Select a Particular report to use this button");
        return false;
    }
    return true;    
}
