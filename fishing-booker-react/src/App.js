import logo from './logo.svg';
import './App.css';
import { useEffect } from 'react';
const axios = require('axios').default;

function App() {
  //anonimna funkcija
  useEffect(() => {
    axios.get("https://localhost:44352/AdminUsers/AllUsers").then((response) => {
      console.log(response)
    })
  }) 
  return (
    <div className="App">
    </div>
  );
}

export default App;
