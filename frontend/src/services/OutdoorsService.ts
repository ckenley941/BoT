import { apiUrl, client } from "../data/DataStore";


  export function getOutdoorsActivities() {
    return client.get(`${apiUrl}/outdoorsactivities`);
  }
