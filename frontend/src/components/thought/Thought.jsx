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

import { getRandomThought, getRelatedThoughts, getThoughtById } from "../../services/ThoughtsService.ts";
import JsonDataGrid from "../controls/JsonDataGrid.jsx";

export default function Thought(props) {
  
    const [thought, setThought] = useState({
        description: "",
        thoughtDateString: "",
        bucket: {
          id: 0,
          description: ""
        },
        details: []
    });
    const [relatedThoughts, setRelatedThoughts] = useState([]);
    const [tabValue, setTabValue] = useState(0);

    useEffect(() => {
        loadData();
      }, [ props.data]);
    
      const loadData = async () => {
        if ( props.data){
          setThought(props.data);
          loadChildren(props.data.id);
        }
        // else if (props.match.params.id > 0){
        //   getThoughtById(props.match.params.id > 0).then((response) => {
        //     setThought(response.data);
        //     loadChildren(response.data.id);
        //   });
        // }
        else{
          // getRandomThought().then((response) => {
          //     setThought(response.data);
          //     loadChildren(response.data.id);
          // });
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
          <Grid md={3} xs={12}>            
          <FormLabel>{thought.thoughtDateString }</FormLabel>  
          </Grid>
          <Grid md={9} xs={12}>
              <FormLabel>{thought.bucket.description}</FormLabel>  
          </Grid>
          <Grid xs={12}>

          <Typography variant="h5">{thought.description }</Typography>  
          </Grid>
          <Grid xs={12}>

          <Tabs value={tabValue} onChange={handleTabChange} textColor="secondary" indicatorColor="secondary"
              aria-label="secondary tabs example">
              <Tab label="Details" />
              <Tab label="Related Thoughts" />
              <Tab label="Website Links" />
            </Tabs>

            <TabPanel value={tabValue} index={0}>
              {thought.textType === "Json" ? (<div>
                  <JsonDataGrid jsonString={thought.details[1].description} columns={thought.details[0].description}></JsonDataGrid>
              </div>) : <>
              {  thought.details.length > 0 ? 
                thought.details.map((t, i) => (
                       <div class="thoughtDetail">{t.description}</div>
                        )) :
                <div>No details</div>}
                </>}
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

