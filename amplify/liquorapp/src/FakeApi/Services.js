  
export async function GetUsers() {
       return await fetch('http://localhost:3001/Users')
      .then(data => data.json()     
      )
  }