
//Eventually configure better - test checkin for redploy
import axios from "axios";

const prodUrl = "https://nts2q9eay1.execute-api.us-west-2.amazonaws.com/Prod/";
const devUrl = "https://localhost:57568/";

export const mainUrl = prodUrl;
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