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

  export function getThoughtsGrid() {
    return client.get(`${apiUrl}/thoughts/grid`);
  }

  export function getThoughtById(id: number) {
    return client.get(`${apiUrl}/thoughts/${id}`);
  }

  export function getRelatedThoughts(id: number) {
    return client.get(`${apiUrl}/thoughts/related/${id}`);
  }

  export function insertThought(newThought) {
    return client.post(`${apiUrl}/thoughts`, newThought);
  }

  export function insertThoughtCategory(newThoughtCategory) {
    return client.post(`${apiUrl}/thoughtcategory`, newThoughtCategory);
  }

  export function updateThoughtCategory(newThoughtCategory, id: number) {
    return client.put(`${apiUrl}/thoughtcategory/${id}`, newThoughtCategory);
  }

  export function deleteThoughtCategory( id: number) {
    return client.delete(`${apiUrl}/thoughtcategory/${id}`);
  }