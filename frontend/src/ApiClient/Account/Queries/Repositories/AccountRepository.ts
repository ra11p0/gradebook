import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

function getHelloWorld():string{
    return 'hello world!';
}

function getApiUrl():string|undefined{
    return API_URL;
}

function getWeather():Promise<any>{
    return axios.get(API_URL + '/Weather');
}

export default {
    getHelloWorld,
    getApiUrl,
    getWeather
}
