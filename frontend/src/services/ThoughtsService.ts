import { apiUrl, client } from "../data/DataStore";

export function getRandomThought() {
    return client.get(`${apiUrl}/thoughts/random`);
  }

  export function getSelectedDashboard(dashboardType: string) {
    return client.get(`${apiUrl}/dashboards/selected?dashboardType=${dashboardType}`);
  }

  export function getThoughtCategories() {
    return client.get(`${apiUrl}/thoughtcategories`);
  }

  export function getThoughts() {
    return client.get(`${apiUrl}/thoughts`);
  }

  export function insertThought(newThought: NewThought) {
    return client.post(`${apiUrl}/thoughts`, newThought);
  }

  export function insertThoughtCategory(newThoughtCategory: NewThoughtCategory) {
    return client.post(`${apiUrl}/thoughtcategory`, newThoughtCategory);
  }
