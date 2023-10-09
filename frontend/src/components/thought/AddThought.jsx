import React, { useState, useEffect } from "react";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormHelperText from '@mui/material/FormHelperText';
import FormControl from '@mui/material/FormControl';
import TextField from '@mui/material/TextField';
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Select from '@mui/material/Select';
import Grid from "@mui/material/Unstable_Grid2";
import Tooltip from '@mui/material/Tooltip';

import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import AddIcon from '@mui/icons-material/Add';
import SaveIcon from '@mui/icons-material/Save';
import DeleteIcon  from "@mui/icons-material/Delete";

import _ from 'lodash';

import { getThoughtCategories, insertThought } from "../../services/ThoughtsService.ts";

export default function AddThought() 
{
  const [thought, setThought] = useState({
    description: "",
    thoughtCategoryId: 0,
    details: []
  })
  const [thoughtCategories, setThoughtCategories] = useState([]);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    getThoughtCategories().then((response) => {
      setThoughtCategories(response.data);
      console.log(response.data);
    });
  };

  //TODO-make this a global function
  const handleInputChange = (e) => {
    let newState = { ...thought };
    const { name, value } = e.target;
    _.set(newState, name, value);
    setThought(newState);
  };

  const handleDetailChange = (e) => {
    var index = parseInt(e.target.name);
    thought.details[index] = e.target.value;
    let newState = { ...thought };
    setThought(newState);
  };


  const addThought = () => {
    insertThought(thought).then((response) => {
      console.log(response.data);
    });
  };

  const addDetail = () => {
    thought.details.push("");
    let newState = { ...thought };
    setThought(newState);
  }

  const deleteDetail = (e) => {
    var index = parseInt(e.currentTarget.name);
    delete thought.details[index];
    let newState = { ...thought };
    setThought(newState);
  }

    return ( 
      <Card variant="outlined"  sx={{ m: 5, maxWidth: 750 }}>
      <CardContent>
        <Grid>
        <TextField  sx={{ m: 1}}
          name="description"
          label="Thought"
          onChange={handleInputChange}
          fullWidth
          multiline
        />
        </Grid>
    <Grid>
    <FormControl sx={{ m: 1, minWidth: 120 }}>
        <InputLabel id="demo-simple-select-helper-label">Category</InputLabel>
        <Select
          name="thoughtCategoryId"
          value={thought.thoughtCategoryId}
          label="Category"
          onChange={handleInputChange}          
        >
          <MenuItem value="0">
            <em>None</em>
          </MenuItem>
          { thoughtCategories.map((tc, i) => (
             <MenuItem key={i} value={tc.thoughtCategoryId}>{tc.description}</MenuItem>
                ))}
        </Select>
        <FormHelperText>Or add a new one *LATER FEATURE*</FormHelperText>
      </FormControl>
    </Grid>     
      <IconButton color="secondary"  aria-label="Add Detail" onClick={addDetail}>
          <AddIcon fontSize="large" />
        </IconButton>   
        { thought.details.map((t, i) => (
          <div>
                      <TextField sx={{ m: 1, width:"80%"}}
          label="Detail"
          multiline
          name={i}
          onChange={handleDetailChange}
        />
        <IconButton name={i} color="secondary" aria-label="Delete" onClick={deleteDetail}>
        <DeleteIcon></DeleteIcon>
      </IconButton>   
      </div>
                ))}
              <IconButton color="secondary" aria-label="Save" onClick={addThought}>
          <SaveIcon fontSize="large"></SaveIcon>
        </IconButton>   
      </CardContent>
    </Card>
    );
}

