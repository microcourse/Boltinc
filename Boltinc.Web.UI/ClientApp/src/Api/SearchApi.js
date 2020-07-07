import axios from 'axios';

export class SearchApi {
    displayName = SearchApi.name

   static search(text) {
        return axios.get('api/search/' + text)
            .then(response => response.data);
    }
}