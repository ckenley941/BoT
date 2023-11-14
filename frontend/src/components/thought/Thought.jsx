import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Box from '@mui/material/Box';
import FormLabel from '@mui/material/FormLabel';
import Grid from "@mui/material/Unstable_Grid2";
import Typography from "@mui/material/Typography";
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import PropTypes from 'prop-types';

import { getRandomThought } from "../../services/ThoughtsService.ts";

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
  <div role="tabpanel" hidden={value !==index} id={`full-width-tabpanel-${index}`}
    aria-labelledby={`full-width-tab-${index}`} {...other}>
    {value === index && (
    <Box sx={{ p: 3 }}>
      <div>{children}</div>
    </Box>
    )}
  </div>
  );
  }

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};

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
    const [value, setValue] = useState(0);

    useEffect(() => {
        loadData();
      }, [data]);
    
      const loadData = async () => {
        if (data){
          setThought(data);
        }
        else{
          getRandomThought().then((response) => {
              setThought(response.data);
          });
        }
      };

      const handleTabChange = (event, newValue) => {
        setValue(newValue);
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

          <Tabs value={value} onChange={handleTabChange} textColor="secondary" indicatorColor="secondary"
              aria-label="secondary tabs example">
              <Tab label="Details" />
              <Tab label="Related Thoughts" />
              <Tab label="Website Links" />
            </Tabs>

            <TabPanel value={value} index={0}>
            { thought.details.map((t, i) => (
                       <div>{t.description}</div>
                        ))}
            </TabPanel>
            <TabPanel value={value} index={1}>
              Related Words
            </TabPanel>
            <TabPanel value={value} index={2}>
              Under construction
            </TabPanel>
          </Grid>
        </Grid>
        </CardContent>
      </Card>
    );
}

