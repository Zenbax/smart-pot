import axios from "axios";

const instance = axios.create({
    baseURL: "http://13.53.174.85/",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
  });


instance.interceptors.request.use((config) => {
    if (localStorage.getItem('token')){
      config.headers["Authorization"] = "Bearer " + localStorage.getItem("token")
    }
    return config
  });

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
    try{
        const response = await instance.post("/auth/register", jsonUserInfoDTO,);
        console.log(response)
        return true
        
    }
    catch(error){
        console.log(error.response.data.message)
        return error.response.data.message
    }
    
}


export async function createPot (paramPotName, paramMachineId, paramPlant, handleNotAuthorized){
    
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
                const response = await instance.post("/pot/create", jsonUserInfoDTO,);
                console.log(response)
                return true
                
            }
            catch(error){
                if(error?.response?.status === 401){
                    console.log("unauthorized error happened")
                    console.log(error.response)
                    handleNotAuthorized("")
                }
                else{
                    console.log(error.response.data)
                    return error.response.data
                }
            }

    
}


export async function createPlant (paramName, paramMinMoisture, paramImage, paramWateringAmount, handleNotAuthorized){
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
    const response = await instance.post("/plant/create", jsonUserInfoDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
        console.log("unauthorized error happened")
        console.log(error.response)
        handleNotAuthorized("")
    }
    else{
        console.log(error.response.data.message)
        return error.response.data.message
    }
}

}

export async function updatePlant (paramName, paramMinMoisture, paramWateringAmount, paramImage, paramInitialName, handleNotAuthorized){
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
    const response = await instance.put("/plant/update/"+paramInitialName, jsonUserInfoDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
        console.log("unauthorized error happened")
        console.log(error.response)
        handleNotAuthorized("")
    }
    else{
        console.log(error.response.data.message)
        return error.response.data.message
    }
}

}

export async function deletePlant(paramPlantName, handleNotAuthorized){
    try{
        const response = await instance.delete("/plant/delete/"+paramPlantName)
        console.log(response)
        return true
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
            return error.response.data.message
        }
    }
    
}


export async function getPotFromId(id, handleNotAuthorized){
    try{
        const response = await instance.get("/pot/get/"+id)
        console.log(response)
        return response.data
    }
    catch(error){
        if(error?.response?.status === 401){
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
            return false
        }
    }
    
}

export async function getPlantByName(paramName, handleNotAuthorized){
    try{
        const response = await instance.get("/plant/get/"+paramName)
        console.log('getPlantByName response:', response.data);
        return response.data
    }
    catch(error){
        if(error?.response?.status === 401){
            handleNotAuthorized()
        }
        else{
            console.log(error.response.data.message)
            return false
        }
    }
    
}

export async function getAllPots(handleNotAuthorized){
    axios.defaults.headers.common["Authorization"] = "Bearer " + localStorage.getItem('token')
    console.log(axios.defaults.headers.common["Authorization"])
    try{
        const response = await instance.get("/pot/get/all");
        console.log(response)
        return response.data.pots
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
        }
    }
    
}

export async function getAllPlants(handleNotAuthorized){
    try{
        const response = await instance.get("/plant/get/all?userId="+localStorage.getItem("userId"))
        console.log(response)
        return response.data.plants
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
        }
    }
    
}

export async function loginUser(email, password, setToken) {
    console.log("Header before login: " + instance.defaults.headers.common["Authorization"])
    var jsonUserInfoDTO = JSON.stringify(
        {
        email: email,
        Password: password
        }
    )
    console.log(jsonUserInfoDTO)


    try{
        const response = await instance.post("/auth/login", jsonUserInfoDTO);
        console.log(response);  
       localStorage.setItem('userEmail', response.data.user.email);
       localStorage.setItem('userId', response.data.user.id);
       localStorage.setItem('token', response.data.token);
       localStorage.setItem('userName',response.data.user.name);
       localStorage.setItem('userLastName',response.data.user.lastName);
       localStorage.setItem('userPhoneNumber',response.data.user.phoneNumber);
       axios.defaults.headers.common["Authorization"] = "Bearer " + response.data.token; //Bliver sat i authProvider men fordi setState er asyncron bliver den nødt til også at blive sat her
       setToken(response.data.token)
       return true
    }
    catch(error){
            console.log(error.response.data.message)
            return error.response.data.message
        }
    
  }


  export async function updatePot (paramName,paramEmail,paramMachineId,paramEnable,paramPlant, paramID, handleNotAuthorized){
    var jsonUserInfoDTO = JSON.stringify(
        {
            potName: paramName,
            email: paramEmail,
            machineId: paramMachineId,
            enable: paramEnable,
            plant: paramPlant

        }
    )
   
try{
    const response = await instance.put("/pot/update/"+paramID, jsonUserInfoDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
        console.log("unauthorized error happened")
        console.log(error.response)
        handleNotAuthorized("")
    }
    else{
        console.log(error.response.data.message)
        return error.response.data.message
    }
}}


export async function deletePot(paramMachineId, handleNotAuthorized){
    try{
        const response = await instance.delete("/pot/delete/"+ paramMachineId)
        console.log(response)
        return true
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log("unauthorized error happened")
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
            return error.response.data.message
        }
    }
    
}

