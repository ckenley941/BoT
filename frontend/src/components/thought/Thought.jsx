import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import FormLabel from '@mui/material/FormLabel';
import Grid from "@mui/material/Unstable_Grid2";
import Typography from "@mui/material/Typography";
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import ThoughtsGrid from './ThoughtsGrid.jsx'
import TabPanel from "../controls/TabPanel.jsx"

import { getRandomThought, getRelatedThoughts } from "../../services/ThoughtsService.ts";


export default function Thought({data}) {
    const [thought, setThought] = useState({
        description: "",
        thoughtDateString: "",
        category: {
          id: 0,
          description: ""
        },
        details: []
    });
    const [relatedThoughts, setRelatedThoughts] = useState([]);
    const [tabValue, setTabValue] = useState(0);

    useEffect(() => {
        loadData();
      }, [data]);
    
      const loadData = async () => {
        if (data){
          setThought(data);
          loadChildren(data.id);
        }
        else{
          getRandomThought().then((response) => {
              setThought(response.data);
              loadChildren(response.data.id);
          });
        }        
      };

      const loadChildren = async (id) => {
        getRelatedThoughts(id).then((response) => {
          setRelatedThoughts(response.data);
      });
      }

      const handleTabChange = (event, newValue) => {
        setTabValue(newValue);
      };

    return (
        <Card variant="outlined">
        <CardContent>
        <Grid container spacing={2}>
          <Grid item md={3} xs={12}>            
          <FormLabel>{thought.thoughtDateString }</FormLabel>  
          </Grid>
          <Grid item md={9} xs={12}>
              <FormLabel>{thought.category.description}</FormLabel>  
          </Grid>
          <Grid item xs={12}>

          <Typography variant="h5">{thought.description }</Typography>  
          </Grid>
          <Grid item xs={12}>

          <Tabs value={tabValue} onChange={handleTabChange} textColor="secondary" indicatorColor="secondary"
              aria-label="secondary tabs example">
              <Tab label="Details" />
              <Tab label="Related Thoughts" />
              <Tab label="Website Links" />
            </Tabs>

            <TabPanel value={tabValue} index={0}>
              
            {  thought.details.length > 0 ? 
                thought.details.map((t, i) => (
                       <div>{t.description}</div>
                        )) :
                <div>No details</div>}
            </TabPanel>
            <TabPanel value={tabValue} index={1}>
              {
                relatedThoughts.length > 0 ?   
                <ThoughtsGrid data={relatedThoughts}></ThoughtsGrid> :
                <div>No related thoughts</div>
              }
            
            </TabPanel>
            <TabPanel value={tabValue} index={2}>
              Under construction
            </TabPanel>
          </Grid>
        </Grid>
        </CardContent>
      </Card>
    );
}

