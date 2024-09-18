import React, { useState, useEffect } from "react";

import { Grid } from "tabler-react";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";

import ThoughtsGrid from './ThoughtsGrid.jsx'
import { getRecentlyAddedThoughts, getRecentlyViewedThoughts } from "../../services/ThoughtsService.ts";


export default function ThoughtBank() {  
    const [recentlyViewedThoughts, setRecentlyViewedThoughts] = useState([]);
    const [recentlyAddedThoughts, setRecentlyAddedThoughts] = useState({});
    const [selectedRecentThoughtsRdo, setSelectedRecentThoughtsRdo]= useState("RecentlyViewed");
    const [isLoadingRecentlyViewed, setIsLoadingRecentlyViewed] = useState(true);
    const [isLoadingRecentlyAdded, setIsLoadingRecentlyAdded] = useState(true);    

    useEffect(() => {
      if (isLoadingRecentlyViewed && selectedRecentThoughtsRdo === "RecentlyViewed"){
          getRecentlyViewedThoughts().then((response) => {
            setRecentlyViewedThoughts(response.data);
            setIsLoadingRecentlyViewed(false);
          });
        }
        else if (isLoadingRecentlyAdded){
          getRecentlyAddedThoughts("RecentThought", 0).then((response) => {
            setRecentlyAddedThoughts(response.data);
            setIsLoadingRecentlyAdded(false);
          });
        } 
      }, [selectedRecentThoughtsRdo, isLoadingRecentlyViewed, isLoadingRecentlyAdded]);
      
  const handleSelectedRecentThoughtsRdoChange = (e) => {
    setSelectedRecentThoughtsRdo(e.target.value);
  }

    return (
       <div>
        <Grid.Col md={4} width={12}>
          <FormControl>
            <RadioGroup
              row
              aria-labelledby="recent-thoughts-radio-buttons-group-label"
              name="recentThoughts"
              value={selectedRecentThoughtsRdo}
              onChange={handleSelectedRecentThoughtsRdoChange}
            >
              <FormControlLabel value="RecentlyViewed" control={<Radio />} label="Recently Viewed" />
              <FormControlLabel value="RecentlyAdded" control={<Radio />} label="Recently Added"  />
            </RadioGroup>
          </FormControl>         
        </Grid.Col>
        {!isLoadingRecentlyViewed && selectedRecentThoughtsRdo === 'RecentlyViewed' ? (
           <ThoughtsGrid key="RecentlyViewed" data={recentlyViewedThoughts}></ThoughtsGrid>) :
           !isLoadingRecentlyAdded && selectedRecentThoughtsRdo === 'RecentlyAdded' ? (
            <ThoughtsGrid key="RecentlyAdded" data={recentlyAddedThoughts}></ThoughtsGrid>) 
            : <></>
        }
       </div>
    );
}

