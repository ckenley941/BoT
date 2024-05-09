import React from "react";


import TextField from '@mui/material/TextField';
import Tooltip from '@mui/material/Tooltip';


import IconButton from "@mui/material/IconButton";
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon  from "@mui/icons-material/Delete";

export default function DetailRow(props){
    return ( 
        <div>
            <IconButton color="secondary"  aria-label="Add" onClick={props.handleAdd}>
<Tooltip title={`Add ${props.title}`}>      
  <AddIcon fontSize="large" />
</Tooltip>
</IconButton>   
{ props.data.map((t, i) => (
    <div>
    <TextField sx={{ m: 1, width:"80%"}} label={props.title} multiline name={i} onChange={props.handleChange} value={t}/>
    <IconButton name={i} color="secondary" aria-label="Delete" onClick={props.handleDelete}>
      <Tooltip title={`Delete ${props.title}`}>      
        <DeleteIcon fontSize="large" />
      </Tooltip>
    </IconButton>  
    </div>
        ) 
    )
    }
    
    </div>
    )
}

