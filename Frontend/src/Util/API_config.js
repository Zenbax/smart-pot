import axios from "axios";
import Cookies from 'js-cookie';
import { useAuth } from "./AuthProvider";

const API_BASE_URL = "http://13.53.174.85/";
var potArray=[
    {
        nameOfPot: "Soveværelses vindue",
        id: 21938,
        plant: {
            nameOfPlant: "Monstera",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
    },
    {
        nameOfPot: "Stue vindue #1",
        id: 87362,
        plant: {
            nameOfPlant: "Philodendron",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
        VandingsInstillinger: {
            JordMinimumsFugtighed: 35,
            vandingsMængdeML: 100
        }
    },
    {
        nameOfPot: "Stue vindue #2",
        id: 73837,
        plant: {
            nameOfPlant: "Lilje",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"

        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 3, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
    }
]


const unauthorizedInstance = axios.create({
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

const authorizedInstance = axios.create({
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
      //Bearer: Cookies.get('token')
      Authorization: 'Bearer '+ localStorage.getItem('token')
    },
  });

  authorizedInstance.interceptors.request.use((config) => {
    if (localStorage.getItem('token')){ // or get it from localStorage
      config.headers["Authorization"] = "Bearer " + localStorage.getItem("token")
    }
    return config
  })

export async function createUser (paramName, paramLastName, paramPassword, paramEmail,paramPhoneNumber){ //TODO: eventuelt parse til JSON et andet sted
        var jsonUserInfoDTO = JSON.stringify(
            {
                name: paramName,
                lastName: paramLastName,
                email: paramEmail,
                password: paramPassword,
                phoneNumber: paramPhoneNumber
            }
        )
        console.log(jsonUserInfoDTO)
    try{
        const response = await unauthorizedInstance.post("/auth/register", jsonUserInfoDTO,);
        console.log(response)
        
    }
    catch(Error){
        console.log(Error.message)
    }
    
}


export async function createPot (paramPotName, paramMachineId, paramPlant){
    
    var paramEnable = 0
        if(paramPlant){
            paramEnable = 1
        }
        var jsonUserInfoDTO = JSON.stringify(
            {
                potName: paramPotName,
                email: localStorage.getItem('userEmail'),
                machineId: paramMachineId,
                enable: paramEnable,
                plant: paramPlant
            });
            try{
                const response = await authorizedInstance.post("/pot/create", jsonUserInfoDTO,);
                console.log(response)
                
            }
            catch(Error){
                console.log(Error.message)
            }

    
}


export async function createPlant (paramName, paramMinMoisture, paramImage, paramWateringAmount){
    var jsonUserInfoDTO = JSON.stringify(
        {
            nameOfPlant: paramName,
            soilMinimumMoisture: paramMinMoisture,
            image: paramImage,
            amountOfWaterToBeGiven: paramWateringAmount,
            userid: localStorage.getItem("userId")
        }
    )
    console.log(jsonUserInfoDTO)
try{
    const response = await authorizedInstance.post("/plant/create", jsonUserInfoDTO,);
    console.log(response)
    
}
catch(Error){
    console.log(Error.message)
}

}

export async function updatePlant (paramName, paramMinMoisture, paramWateringAmount, paramImage, paramInitialName){
    var jsonUserInfoDTO = JSON.stringify(
        {
            nameOfPlant: paramName,
            soilMinimumMoisture: paramMinMoisture,
            amountOfWaterToBeGiven: paramWateringAmount,
            image: paramImage
        }
    )
    console.log(jsonUserInfoDTO)
try{
    const response = await authorizedInstance.put("/plant/update/"+paramInitialName, jsonUserInfoDTO,);
    console.log(response)
    
}
catch(Error){
    console.log(Error.message)

}

}

export async function deletePlant(paramPlantName){
    try{
        const response = await authorizedInstance.delete("/plant/delete/"+paramPlantName)
        console.log(response)
    }
    catch(Error){
        console.log(Error.message)
    }
    
}


export async function getPotFromId(id){
    try{
        const response = await authorizedInstance.get("/pot/get/"+id)
        console.log(response)
        return response.data
    }
    catch(error){
        if(error?.response?.status === 401){
            notAuthorized()
        }
    }
    
}

export async function getAllPots(handleNotAuthorized){
    axios.defaults.headers.common["Authorization"] = "Bearer " + localStorage.getItem('token')
    console.log(axios.defaults.headers.common["Authorization"])
    try{
        const response = await authorizedInstance.get("/pot/get/all");
        console.log(response)
        return response.data.pots
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            console.log(error.response)
            handleNotAuthorized()
        }
    }
    
}

export async function getAllPlants(){
    try{
        const response = await authorizedInstance.get("/plant/get/all?userId="+localStorage.getItem("userId"))
        console.log(response)
        return response.data.plants
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            notAuthorized()
        }
    }
    
}

export async function loginUser(email, password, setToken) {
    console.log("Header before login: " + authorizedInstance.defaults.headers.common["Authorization"])
    var jsonUserInfoDTO = JSON.stringify(
        {
        email: email,
        Password: password
        }
    )
    console.log(jsonUserInfoDTO)


    try{
        const response = await unauthorizedInstance.post("/auth/login", jsonUserInfoDTO);
        console.log(response);  
       localStorage.setItem('userEmail', response.data.user.email)
       localStorage.setItem('userId', response.data.user.id);
       localStorage.setItem('token', response.data.token)
       axios.defaults.headers.common["Authorization"] = "Bearer " + response.data.token; //Bliver sat i authProvider men fordi setState er asyncron bliver den nødt til også at blive sat her
       setToken(response.data.token)
       return true
    }
    catch(error){
        if(error?.response?.status === 400){
            console.log("Invalid Credentials error happened")
            return "Invalid Credentials"
        }
    }
    
  }

  function notAuthorized(){
    const { setToken } = useAuth();
    console.log("Removing token")
    setToken("");
  }



  export async function updatePot (paramName,paramEmail,paramMachineId,paramEnable,paramPlant,  paramInitialName){
    var jsonUserInfoDTO = JSON.stringify(
        {
            potName: paramName,
            email: paramEmail,
            machineId: paramMachineId,
            enable: paramEnable,
            plant: paramPlant

        }
    )
    console.log(jsonUserInfoDTO)
try{
    const response = await authorizedInstance.put("/pot/update/"+paramInitialName, jsonUserInfoDTO,);
    console.log(response)
    
}
catch(Error){
    console.log(Error.message)

}}


export async function deletePot(){
    try{
        const response = await authorizedInstance.delete("/pot/delete/"+ paramMachineId)
        console.log(response)
    }
    catch(Error){
        console.log(Error.message)
    }
    
}