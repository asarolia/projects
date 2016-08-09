var dtCh= "/";
var minYear=1990;
var maxYear=2100;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}

function isDateOld(dtStr){//No more used
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		//alert("The date format should be : mm/dd/yyyy")
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		//alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		//alert("Please enter a valid day")
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		//alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear)
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		//alert("Please enter a valid date")
		return false
	}
return true
}

function ValidateForm(dt){
	if (isDate(dt.value)==false){
		dt.focus()
		return false
	}
    return true
 }

function isValidUKDate(dtStr){
    dtCh="-";
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strYear=dtStr.substring(0,pos1)
	var strMonth=dtStr.substring(pos1+1,pos2)
	var strDay=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		//alert("The date format should be : mm/dd/yyyy")
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		//alert("Please enter a valid month")
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		//alert("Please enter a valid day")
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		//alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear)
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		//alert("Please enter a valid date")
		return false
	}
return true
}


//New script for date check on Search page
function isDate(dateValue)
{

    var value = trimString(dateValue);
     if(value.length == 10)
		            {
			            var patt=/([0-9]{2}.[0-9]{2}.[0-9]{4})/g;
			            var pattNext=/([0-9]{2}\/[0-9]{2}\/[0-9]{4})/g;
			            var result=patt.test(value);
			            if (result)
			            {
				            value = value.substring(6,10) + "-" + value.substring(3,5) + "-" + value.substring(0,2) ;				            
				            if(isValidDate(value))
				            {
				                //document.getElementById(sourceControl).value = value;
					            return true;
				            }
			            }
			            else if(pattNext.test(value))
			            {
			                value = value.substring(6,10) + "-" + value.substring(3,5) + "-" + value.substring(0,2) ;				            
				            if(isValidDate(value))
				            {
				                //document.getElementById(sourceControl).value = value;
					            return true;
				            }
			            }
			            else
			            {
				            var nextpatt=/([0-9]{4}-[0-9]{2}-[0-9]{2})/g;
				            var newresult = nextpatt.test(value);
				            if(newresult)
				            {
					            if(isValidDate(value))
						            {
							            return true;
						            }
				            }
			            }
		            }
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
