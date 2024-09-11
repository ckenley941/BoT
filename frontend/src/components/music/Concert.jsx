import React, { useState, useEffect } from "react";

import _ from "lodash";

import { Grid } from "tabler-react";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";

import Setlist from "./Setlist.jsx"

export default function Concert(props) {
  const [formState, setFormState] = useState({
    bandName: "",
    venue: "",
    concertDate: "",
    concertDayOfWeek: "",
    concertUrl: "",
    notes: "",
    songs: [],
    setlist: []
  });

  useEffect(() => {
    if (props.data) {
      setFormState(props.data);
      console.log(props.data);
    }
  }, []);

  return (
    <Card variant="outlined">
      <CardContent>
        <Grid container>
          <Grid.Row className="mt-2">
            <Grid.Col md={3} width={12}>
              <FormControl fullWidth>
                <InputLabel id="concert-date-label">
                  {formState.concertDate}{", "}
                  {formState.concertDayOfWeek}
                </InputLabel>
              </FormControl>
            </Grid.Col>
            <Grid.Col md={9} width={12}>
              <FormControl fullWidth>
                <InputLabel id="band-name-label">
                  {formState.bandName}
                </InputLabel>
              </FormControl>
            </Grid.Col>
          </Grid.Row>
          <Grid.Row className="mt-2">
            <Grid.Col md={12} width={12}>
              <Typography
                id="venue-label"
                variant="h5"
                className="ml-3 mt-5"
              >
                {formState.venue}
              </Typography>
              {/* <Link
                href={formState.concertUrl}
                className="ml-3 mt-4"
                target="_blank"
                rel="noreferrer"
              >
                Concert Link
              </Link> */}
            </Grid.Col>
          </Grid.Row>
          <Grid.Row className="mt-2 ml-3">
          <Grid.Col md={12} width={12}>
            {  formState.setlist.length > 0 ? 
                
                formState.setlist.map((s, i) => (
                       
            <Setlist data={s}></Setlist>
                        )) 
                        :
                <></>}
                </Grid.Col>
                </Grid.Row>
                       {formState.notes && formState.notes.length > 0 && (
            <Grid.Row className="mt-5 ml-3">              
              {formState.notes}
            </Grid.Row>
          )}      
        </Grid>
      </CardContent>
    </Card>
  );
}
