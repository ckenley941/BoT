import React, { useState, useEffect } from "react";

import _ from "lodash";

import { Grid } from "tabler-react";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";

export default function OutdoorActivity(props) {
  const [formState, setFormState] = useState({
    activityName: "",
    activityType: "",
    activityDate: "",
    geographicArea: "",
    activityLength: "",
    elevationGain: "",
    totalTimeHours: "",
    totalTimeMinutes: "",
    movingTimeHours: "",
    movingTimeMinutes: "",
    activityUrl: "",
  });

  useEffect(() => {
    if (props.data) {
      setFormState(props.data);
    }
  }, []);

  const getLengthLabel = () => {
    switch (formState.activityType) {
      case "Skiing":
        return "lifts";
      case "Camping":
        return "days";
      default:
        return "miles";
    }
  };

  const getElevationGainLabel = () => {
    switch (formState.activityType) {
      case "Skiing":
        return "vertical feet";
      default:
        return "feet elevation gain";
    }
  };

  return (
    <Card variant="outlined">
      <CardContent>
        <Grid container>
          <Grid.Row className="mt-2">
            <Grid.Col md={3} width={12}>
              <FormControl fullWidth>
                <InputLabel id="activity-date-label">
                  {formState.activityDate}
                </InputLabel>
              </FormControl>
            </Grid.Col>
            <Grid.Col md={9} width={12}>
              <FormControl fullWidth>
                <InputLabel id="activity-type-label">
                  {formState.activityType}
                </InputLabel>
              </FormControl>
            </Grid.Col>
          </Grid.Row>
          <Grid.Row className="mt-2">
            <Grid.Col md={12} width={12}>
              <Typography
                id="activity-name-label"
                variant="h5"
                className="ml-3 mt-5"
              >
                {formState.activityName}
              </Typography>
              <Link
                href={formState.activityUrl}
                className="ml-3 mt-4"
                target="_blank"
                rel="noreferrer"
              >
                Activity Link
              </Link>
            </Grid.Col>
          </Grid.Row>
          {formState.geographicArea.length > 0 && (
            <Grid.Row>
              <Grid.Col md={6} width={12}>
                <FormControl fullWidth>
                  <InputLabel id="activity-geographic-area-label">
                    {formState.geographicArea}
                  </InputLabel>
                </FormControl>
              </Grid.Col>
            </Grid.Row>
          )}
          <Grid.Row className="mt-2">
            {formState.activityLength > 0 && (
              <Grid.Col md={2} width={12}>
                <FormControl fullWidth>
                  <InputLabel id="activity-length-label">
                    {formState.activityLength} {getLengthLabel()}
                  </InputLabel>
                </FormControl>
              </Grid.Col>
            )}
            {formState.elevationGain > 0 && (
              <Grid.Col md={3} width={12}>
                <FormControl fullWidth>
                  <InputLabel id="elevation-gain-label">
                    {formState.elevationGain} {getElevationGainLabel()}
                  </InputLabel>
                </FormControl>
              </Grid.Col>
            )}
            {(formState.movingTimeHours > 0 ||
              formState.movingTimeMinutes > 0) && (
              <Grid.Col md={3} width={12}>
                <FormControl fullWidth>
                  <InputLabel id="moving-time-label">
                  Moving Time:{" "}
                    {formState.movingTimeHours}h{" "}
                    {formState.movingTimeMinutes}m
                  </InputLabel>
                </FormControl>
              </Grid.Col>
            )}
            {(formState.totalTimeHours > 0 ||
              formState.totalTimeMinutes > 0) && (
              <Grid.Col md={3} width={12}>
                <FormControl fullWidth>
                  <InputLabel id="total-time-label">
                  Total Time:{" "} 
                    {formState.totalTimeHours}h{" "}
                    {formState.totalTimeMinutes}m
                  </InputLabel>
                </FormControl>
              </Grid.Col>
            )}
          
          </Grid.Row>
        </Grid>
      </CardContent>
    </Card>
  );
}
