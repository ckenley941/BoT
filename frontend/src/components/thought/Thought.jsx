import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Box from '@mui/material/Box';
import FormLabel from '@mui/material/FormLabel';
import Grid from "@mui/material/Unstable_Grid2";
import Typography from "@mui/material/Typography";

import { getRandomThought } from "../../services/ThoughtsService.ts";

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
          { thought.details.map((t, i) => (
                        <Grid key={i}>
                          <div>{t.description}</div>
                        </Grid>
                        ))}
          </Grid>
        </Grid>
        </CardContent>
      </Card>
    );
}

