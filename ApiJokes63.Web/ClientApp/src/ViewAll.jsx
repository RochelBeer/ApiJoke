import axios from "axios";
import React, { useEffect, useState } from "react";
import Joke from "./Joke";

const ViewAll = ()=>{

const [jokes, setJokes] = useState([]) 

useEffect(()=>{
    getAll();
})
const getAll =  async ()=>{
const {data} = await axios.get('/api/jokes/viewall')
setJokes(data)
}
return(
    <div className="row">
  <div className="col-md-6 offset-md-3">
 {  jokes.map(j => <Joke joke={j}/>)}   
  </div>
</div>

)

}
export default ViewAll