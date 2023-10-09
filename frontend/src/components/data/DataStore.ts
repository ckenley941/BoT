
//Eventually configure better
import axios from "axios"; //https://www.npmjs.com/package//axios

//const prodUrl = "";
const devUrl = "http://localhost:5000/";

export const mainUrl = devUrl;
export const apiUrl = mainUrl + "api";

export const client = axios.create({
    baseURL: apiUrl,
    responseType: "json",
    headers: {
      "Content-Type": "application/json",
    },
  });

  //defaults.headers.common['Authorization'] = AUTH_TOKEN;
  
//   client.interceptors.request.use((config) => {
//     const token = localStorage.getItem("access_token");
//     config.headers.Authorization = "Bearer " + token;
//     return config;
//   });