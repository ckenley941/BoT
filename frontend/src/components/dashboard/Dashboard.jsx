import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Unstable_Grid2";
import Typography from "@mui/material/Typography";
import Thought from '../thought/Thought.jsx'
import WordCard from "../word/WordCard.jsx";
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import RefreshIcon from '@mui/icons-material/Refresh';
import IconButton from "@mui/material/IconButton";
import SelectedDashboard from "./SelectedDashboard.jsx";
import { getSelectedDashboard } from "../../services/ThoughtsService.ts";
import { getThoughtCategories } from "../../services/ThoughtsService.ts";

export default function Dashboard() {
    const [currentlySelectedDashboard, setCurrentlySelectedDashboard] = useState("RandomThought");
    const [data, setData] = useState(null);
    const [showCategories, setShowCategories] = useState(true); //setting to true for now while defaulting to RandomThought
    const [thoughtCategories, setThoughtCategories] = useState([]);

    useEffect(() => {
      loadCategories();
    }, []);
  
    const loadCategories = async () => {
      getThoughtCategories().then((response) => {
        setThoughtCategories(response.data);
      });
    };

    const handleInputChange = (e) => {
        setData(null);
        setShowCategories(e.target.value === "RandomThought");
        setCurrentlySelectedDashboard(e.target.value);
      }

      const refresh = () => {        
          getSelectedDashboard(currentlySelectedDashboard).then((response) => {
            setData(response.data.data[0]);
        });
      };

    return (
        <Card variant="outlined">          
        <CardContent>
        <Select
          label="Selected Dashboard Panel"
          onChange={handleInputChange}   
          value={currentlySelectedDashboard}   
        >
         <MenuItem value="RandomThought">
            <em>Random Thought</em>
          </MenuItem>
          <MenuItem value="RandomWord">
            <em>Random Word</em>
          </MenuItem>
        </Select>    
        { showCategories ?
        <span class="padLeft">
          <Select
            name="thoughtCategoryId"
            value="0"
            label="Category"
            onChange={handleInputChange}          
          >
             <MenuItem value="0">
              <em>All Categories </em>
            </MenuItem>
            { thoughtCategories.map((tc, i) => (
               <MenuItem key={i} value={tc.id}>{tc.description}</MenuItem>
                  ))}
          </Select> </span> : ""
          
        }
        <IconButton color="secondary" aria-label="Refresh" onClick={refresh}>
            <RefreshIcon />
          </IconButton>    
          <SelectedDashboard selected={currentlySelectedDashboard} data={data}></SelectedDashboard> 
        </CardContent>
      </Card>
    );
}

