import React, { useState, useEffect } from "react";

import ThoughtsGrid from './ThoughtsGrid.jsx'
import { getThoughtBank } from "../../services/ThoughtsService.ts";

export default function ThoughtBank() {  
    const [thoughtBank, setThoughtBank] = useState([]);

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getThoughtBank().then((response) => {
            setThoughtBank(response.data);
        });  
      };   

    return (
       <div>
        {thoughtBank.length > 0 ?   
            <ThoughtsGrid data={thoughtBank}></ThoughtsGrid> :
            <div className="m-3">No thoughts in the bank</div>
        }       
       </div>
    );
}

