import { apiUrl, client } from "../data/DataStore";

export function getRandomThought() {
    return client.get(`${apiUrl}/thoughts/random`);
  }

  export function getThoughtCategories() {
    return client.get(`${apiUrl}/thoughtcategories`);
  }

  export function insertThought(newThought: NewThought) {
    return client.post(`${apiUrl}/thoughts`, newThought);
  }
