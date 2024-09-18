import { apiUrl, client } from "../data/DataStore";

export function getRandomThought() {
    return client.get(`${apiUrl}/thoughts/random`);
  }

  export function getSelectedDashboard(dashboardType: string, selectedBucketId: number) {
    return client.get(`${apiUrl}/dashboards/selected?dashboardType=${dashboardType}&thoughtBucketId=${selectedBucketId}`);
  }

  export function getThoughtBuckets() {
    return client.get(`${apiUrl}/thoughtbuckets`);
  }

  export function getThoughtsGrid() {
    return client.get(`${apiUrl}/thoughts/grid`);
  }

  export function getThoughtById(id: number) {
    return client.get(`${apiUrl}/thoughts/${id}`);
  }

  export function getRecentlyAddedThoughts() {
    return client.get(`${apiUrl}/thoughts/recentlyadded`);
  }

  export function getRecentlyViewedThoughts() {
    return client.get(`${apiUrl}/thoughts/recentlyviewed`);
  }

  export function getRelatedThoughts(id: number) {
    return client.get(`${apiUrl}/thoughts/related/${id}`);
  }

  export function insertThought(newThought) {
    return client.post(`${apiUrl}/thoughts`, newThought);
  }

  export function insertThoughtBucket(newThoughtBucket) {
    return client.post(`${apiUrl}/thoughtbucket`, newThoughtBucket);
  }

  export function updateThoughtBucket(newThoughtBucket, id: number) {
    return client.put(`${apiUrl}/thoughtbucket/${id}`, newThoughtBucket);
  }

  export function deleteThoughtBucket( id: number) {
    return client.delete(`${apiUrl}/thoughtbucket/${id}`);
  }