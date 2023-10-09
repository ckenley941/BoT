
//Eventually configure better
import axios from "axios";

//const prodUrl = "";
const devUrl = "https://localhost:44384/";

export const mainUrl = devUrl;
export const apiUrl = mainUrl + "api";

export const client = axios.create({
    baseURL: apiUrl,
    responseType: "json",
    headers: {
      "Content-Type": "application/json",
    },
  });
  
//   client.interceptors.request.use((config) => {
//     const token = localStorage.getItem("access_token");
//     config.headers.Authorization = "Bearer " + token;
//     return config;
//   });