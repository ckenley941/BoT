import { apiUrl, client } from "../data/DataStore";

export function getWordById(id: number) {
    return client.get(`${apiUrl}/words/${id}`);
  }

export function getRandomWord() {
    return client.get(`${apiUrl}/words/random`);
  }

export function getWordTranslations(id: number) {
    return client.get(`${apiUrl}/words/GetTranslations/${id}`);
  }

export function getWordTranslationsWithContext(id: number) {
    return client.get(`${apiUrl}/words/GetTranslationsWithContext/${id}`);
  }

export function getWordRelationships(id: number) {
    return client.get(`${apiUrl}/words/GetWordRelationships/${id}`);
  }
  