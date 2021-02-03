import React, { Component } from 'react'
import Menu from './Views/Menu'
import Footer1 from './Views/Footer1'

export class App extends Component {
 
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <Menu />
        </header>
     
        <footer>
          <Footer1 />
        </footer>
      </div>
    )
  }


}


export default App;
