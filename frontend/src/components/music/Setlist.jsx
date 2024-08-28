import React, { useState, useEffect } from "react";

import _ from "lodash";

import { Grid } from "tabler-react";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";

export default function Setlist(props) {
  const [formState, setFormState] = useState({
    //setlist: [],
    setNo: "",
    setlistBody: "",
    // name: "",
    // songLength: "",
    // hasCarrot: "",
    // showGap: "",
    // showGapLastPlayedDate: ""
  });

  useEffect(() => {
    if (props.data) {
      setFormState(props.data);
    }
  }, []);

  const getSetlistLabel = () => {
    if (formState.setNo !== "E"){
        return "Set " + formState.setNo;
    }
    else{
        return "Encore"
    }
  };

  return (
        <Grid container>
            <Grid.Row className="mt-2">
                <h6>
                {getSetlistLabel()}
                </h6>
            </Grid.Row>
          <Grid.Row>            
        { formState.setlistBody}
            {/* <Grid.Col md={3} width={12}>
              <FormControl fullWidth>
                <InputLabel id="set-song-label">
                
                  {formState.setNo}{", "}
                  {formState.songNo}
                </InputLabel>
              </FormControl>
            </Grid.Col>
            <Grid.Col md={9} width={12}>
              <FormControl fullWidth>
                <InputLabel id="song-name-label">
                  {formState.name}
                </InputLabel>
              </FormControl>
            </Grid.Col>
          </Grid.Row>
          <Grid.Row className="mt-2">
            <Grid.Col md={12} width={12}>
              <Typography
                id="song-length-label"
                variant="h5"
                className="ml-3 mt-5"
              >
                {formState.songLength}
              </Typography>              
            </Grid.Col> */}
          </Grid.Row>        
        </Grid>
  );
}
