import { apiUrl, client } from "../data/DataStore";


  export function getOutdoorActivityTypes() {
    return client.get(`${apiUrl}/outdooractivitytypes`);
  }

  export function getOutdoorActivityLogs() {
    return client.get(`${apiUrl}/outdooractivitylogs`);
  }

  export function insertOutdoorActivityLog(outdoorActivityLog) {
    return client.post(`${apiUrl}/outdooractivitylog`, outdoorActivityLog);
  }
