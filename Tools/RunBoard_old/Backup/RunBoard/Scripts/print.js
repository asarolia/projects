  
    var clrs = ConnectPrintDB();
    alert(clrs);
    var cpp = document.getElementById("cpp");
    var ctx = cpp.getContext("2d");
    ctx.beginPath();
    ctx.arc(95, 50, 30, 0, 2 * Math.PI);
    if(clrs == "red"){ 
    ctx.fillStyle = "#FF0000";
    }else{
      if(clrs == "amber"){ 
      ctx.fillStyle = "#FFBE00";
      }
      else{
          if(clrs == "gray"){
           ctx.fillStyle = "#FFFF00";
          }else
          {
           ctx.fillStyle = "#00FF00";
          }
       }
     }
    
    ctx.fill();
