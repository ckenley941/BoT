import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
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
        <Typography variant="h3">{thought.description }</Typography>  
        <Typography variant="h5">{thought.thoughtDateString }</Typography>  
        <Typography variant="h5">Category: {thought.category.description }</Typography>  
            { thought.details.map((t, i) => (
                <Grid key={i}>
                  <div>{t.description}</div>
                </Grid>
                ))}
        </CardContent>
      </Card>
    );
}

