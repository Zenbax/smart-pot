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

export async function createUser (paramName, paramLastName, paramPassword, paramEmail,paramPhoneNumber){ 
        var jsonDTO = JSON.stringify(
            {
                name: paramName,
                lastName: paramLastName,
                email: paramEmail,
                password: paramPassword,
                phoneNumber: paramPhoneNumber
            }
        )
    try{
        const response = await instance.post("/auth/register", jsonDTO,);
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
        var jsonDTO = JSON.stringify(
            {
                potName: paramPotName,
                email: JSON.parse(localStorage.getItem('user')).email,
                machineId: paramMachineId,
                enable: paramEnable,
                plant: paramPlant
            });
            try{
                const response = await instance.post("/pot/create", jsonDTO,);
                console.log(response)
                return true
                
            }
            catch(error){
                if(error?.response?.status === 401){
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
    var jsonDTO = JSON.stringify(
        {
            nameOfPlant: paramName,
            soilMinimumMoisture: paramMinMoisture,
            image: paramImage,
            amountOfWaterToBeGiven: paramWateringAmount,
            userid: JSON.parse(localStorage.getItem('user')).id,
        }
    )
try{
    const response = await instance.post("/plant/create", jsonDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
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
    var jsonDTO = JSON.stringify(
        {
            nameOfPlant: paramName,
            soilMinimumMoisture: paramMinMoisture,
            amountOfWaterToBeGiven: paramWateringAmount,
            image: paramImage
        }
    )
try{
    const response = await instance.put("/plant/update/"+paramInitialName, jsonDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
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
    try{
        const response = await instance.get("/pot/get/all");
        console.log(response)
        return response.data.pots
    }
    catch(error){
        if(error?.response?.status === 401){
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
        const response = await instance.get("/plant/get/all?userId="+JSON.parse(localStorage.getItem('user')).id,)
        console.log(response)
        return response.data.plants
    }
    catch(error){
        if(error?.response?.status === 401){
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
        }
    }
    
}

export async function loginUser(email, password, setToken) {
    var jsonDTO = JSON.stringify(
        {
        email: email,
        Password: password
        })
    try{
        const response = await instance.post("/auth/login", jsonDTO);
        console.log(response);  
        localStorage.setItem('user', JSON.stringify(response.data.user))
        localStorage.setItem('token', response.data.token);
        setToken(response.data.token)
        return true
    }
    catch(error){
            console.log(error.response.data.message)
            return error.response.data.message
        }
    }


  export async function updatePot (paramName,paramEmail,paramMachineId,paramEnable,paramPlant, paramID, handleNotAuthorized){
    var jsonDTO = JSON.stringify(
        {
            potName: paramName,
            email: paramEmail,
            machineId: paramMachineId,
            enable: paramEnable,
            plant: paramPlant

        }
    )
   
try{
    const response = await instance.put("/pot/update/"+paramID, jsonDTO,);
    console.log(response)
    return true
    
}
catch(error){
    if(error?.response?.status === 401){
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
            console.log(error.response)
            handleNotAuthorized("")
        }
        else{
            console.log(error.response.data.message)
            return error.response.data.message
        }
    }
    
}

