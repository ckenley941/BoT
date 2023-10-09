import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Unstable_Grid2";
import Typography from "@mui/material/Typography";

import { getRandomThought } from "../../services/ThoughtsService.ts";

export default function Thought() {
    const [thought, setThought] = useState({
        description: "",
        thoughtDetails: []
    });

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getRandomThought().then((response) => {
            setThought(response.data);
        });
      };

    return (
        <Card variant="outlined">
        <CardContent>
        <Typography variant="h5">  {thought.description }</Typography>  
            { thought.thoughtDetails.map((t, i) => (
                <Grid key={i}>
                  <div>{t.description}</div>
                </Grid>
                ))}
        </CardContent>
      </Card>
    );
}

