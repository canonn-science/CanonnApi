function getRuins() { 
  var urlSystemRuinList = 'https://api.canonn.technology/api/v1/maps/systemoverview'
  var response = UrlFetchApp.fetch(urlSystemRuinList, {'muteHttpExceptions': true});
  var systemData = JSON.parse(response.getContentText());
  var results = [];

  for (s in systemData){
    var sysData = systemData[s]
    var sysName = sysData['systemName'];
    var ruins = sysData['ruins'];
  
    //Exclude reference site
    if(sysName.substring(0,2) != 'z.'){
      //Ruins
      for(r in ruins){
        var ruinData = ruins[r];
        var ruinId = ruinData['ruinId'];
        var ruinType = ruinData['ruinTypeName'];
        var ruinBody = ruinData['bodyName'];
        var ruinCoords = ruinData['coordinates'].join(' // ');
        
        results[ruinId-1] = ['GS' + ruinId,sysName,ruinBody,ruinType,ruinCoords];
      }
    }   
  }

  return results;
}