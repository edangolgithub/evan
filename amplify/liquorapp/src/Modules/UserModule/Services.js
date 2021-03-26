  
export async function GetUsers() {   
    return await fetch('https://nxopo5t28l.execute-api.us-east-1.amazonaws.com/Prod/api/user')
      .then(data => data.json(),
     
      )
  }