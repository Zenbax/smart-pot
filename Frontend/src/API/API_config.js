import axios from "axios";

const API_BASE_URL = "https://localhost:7216"; //TODO: Nok en anden ip

const instance = axios.create({ //TODO: Man skal muligvis download axios: npm install axios@0.24.0
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

export async function createUser (username, password){ //TODO: eventuelt parse til JSON et andet sted
        var jsonUserInfoDTO = JSON.stringify(
            {
                Username: username,
                Password: password,
                Email: 'dummy@email.com',
                Number: 83473984
            }
        )
        console.log(jsonUserInfoDTO);
    /*try{
        const response = await instance.post("/users", jsonUserInfoDTO)
        return response
    }
    catch{
        //TODO: ErrorHandling 
    }*/
    
}

export async function loginUser(username, password) {
    const userInfoDTO ={
        Username: username,
        Password: password
    }
    var jsonUserInfoDTO
    JSON.stringify(userInfoDTO, jsonUserInfoDTO)
    try{
       response = instance.post("/Auth/login", jsonUserInfoDTO)
       return response
    }
    catch{
        //TODO: ErrorHandling 
    }
    
  }