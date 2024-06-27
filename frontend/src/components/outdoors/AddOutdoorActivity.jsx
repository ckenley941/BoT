import React, { useState, useEffect } from "react";

import _ from "lodash";
import moment from "moment";
import dayjs from 'dayjs';

import Select from "@mui/material/Select";
import { Grid } from "tabler-react";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";

import IconButton from "@mui/material/IconButton";
import SaveIcon from "@mui/icons-material/Save";
import TextField from "@mui/material/TextField";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

import { getOutdoorActivityTypes, insertOutdoorActivityLog } from "../../services/OutdoorsService.ts";

export default function AddOutdoorActivity() {
  const [formState, setFormState] = useState({
    activityName: "",
    activityType: "",
    activityDate: dayjs(new Date()),
    geographicArea: "",
    activityLength: "",
    elevationGain: "",
    totalTimeHours: "",
    totalTimeMinutes: "",
    movingTimeHours: "",
    movingTimeMinutes: "",
    activityUrl: "",
  });
  const [outdoorActivityTypes, setOutdoorActivityTypes] = useState([]);

  useEffect(() => {
    loadOutdoorActivityTypes();
  }, []);

  const loadOutdoorActivityTypes = async () => {
    getOutdoorActivityTypes().then((response) => {
      setOutdoorActivityTypes(response.data);
    });
  };

  const handleInputChange = (e) => {
    let newState = { ...formState };
    const { name, value } = e.target;
    _.set(newState, name, value);
    setFormState(newState);
  };

  const handleActivityDateChange = (e) => {
    let newState = { ...formState };
    _.set(newState, "activityDate", e);
    setFormState(newState);
  };

  const addOutdoorActivity = () => {
    var formToSave = {...formState};
    formToSave.activityDate = moment(formState.activityDate.$d).format(
      "YYYY-MM-DD"
    );
    if (isValid()) {
      insertOutdoorActivityLog(formToSave).then((response) => {
        alert("Activity added");
        setFormState({
          activityName: "",
          activityType: "",
          activityDate: dayjs(new Date()),
          geographicArea: "",
          activityLength: "",
          elevationGain: "",
          totalTimeHours: "",
          totalTimeMinutes: "",
          movingTimeHours: "",
          movingTimeMinutes: "",
          activityUrl: "",
        });
      });
    }
  };

  const isValid = () => {
    var msg = "";

    if (formState.activityType.length <= 0) {
      msg += "Activity Type is required. ";
    }

    if (formState.activityName.length <= 0) {
      msg += "Activity Name is required. ";
    }

    if (msg.length > 0) {
      alert(msg);
      return false;
    } 

    return true;
  };

  const getLengthLabel = () => {
    switch (formState.activityType) {
      case "Skiing":
        return "Lifts";
      case "Camping":
        return "Length (Days)";
      default:
        return "Length (Mi)";
    }
  };

  const getActivityNameLabel =  () => {
    switch (formState.activityType) {
      case "Skiing":
        return "Ski Resort *";
      case "Camping":
        return "Campsite *";
      default:
        return "Trail Name *";
    }
  };

  return (
    <Grid container>
      <Grid.Row className="m-2">
        <Grid.Col md={4} xs={12} width={12} className="mt-2">
          <FormControl fullWidth>
            <InputLabel id="activity-type-select-label">
              Activity Type *
            </InputLabel>
            <Select
              labelId="activity-type-select-label"
              id="activity-name-select"
              name="activityType"
              label="Activty Type"
              maxWidth
              value={formState.activityType}
              onChange={handleInputChange}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {outdoorActivityTypes.map((oa, i) => (
                <MenuItem key={i} value={oa}>
                  {oa}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid.Col>
        <Grid.Col md={4} xs={12} width={12} className="mt-2">
          <DatePicker 
            name="activityDate"
            value={formState.activityDate}
            label="Activity Date"
            onChange={handleActivityDateChange}
          />
        </Grid.Col>
      </Grid.Row>
      <Grid.Row className="m-2">
        <Grid.Col md={8} width={12}>
          <TextField
            sx={{ width: "85%" }}
            name="activityName"
            value={formState.activityName}
            label={getActivityNameLabel()}
            onChange={handleInputChange}
            multiline
          />
        </Grid.Col>
      </Grid.Row>
      <Grid.Row className="m-2">
        <Grid.Col md={8} width={12}>
          <TextField
            sx={{ width: "85%" }}
            name="geographicArea"
            value={formState.geographicArea}
            label="Geographic Area"
            onChange={handleInputChange}
            multiline
          />
        </Grid.Col>
      </Grid.Row>
      <Grid.Row className="m-2">
        <Grid.Col md={2} width={12}>
          <TextField
            name="activityLength"
            label={getLengthLabel()}
            value={formState.activityLength.length > 0 ? Number(formState.activityLength) : null}
            onChange={handleInputChange}
            type="number"
          />
        </Grid.Col>
        {formState.activityType !== "Camping" && (
          <Grid.Col md={4} width={12}>
            <TextField
              name="elevationGain"
              label={
                formState.activityType === "Skiing"
                  ? "Vertical Feet"
                  : "Elevation Gain (Ft)"
              }
              value={formState.elevationGain.length > 0 ? Number(formState.elevationGain) : null}
              onChange={handleInputChange}
              type="number"
            />
          </Grid.Col>
        )}
      </Grid.Row>
      {formState.activityType !== "Camping" && (
        <Grid.Row className="m-2">
          <Grid.Col md={2} width={12}>
            <TextField
              name="movingTimeHours"
              label="Moving Time (Hours)"
              value={formState.movingTimeHours.length > 0 ? parseInt(formState.movingTimeHours) : null}
              onChange={handleInputChange}
              type="number"
            />
          </Grid.Col>
          <Grid.Col md={2} width={12}>
            <TextField
              name="movingTimeMinutes"
              label="Moving Time (Minutes)"
              value={formState.movingTimeMinutes.length > 0 ? parseInt(formState.movingTimeMinutes) : null}
              onChange={handleInputChange}
              type="number"
            />
          </Grid.Col>
          <Grid.Col md={2} width={12}>
            <TextField
              name="totalTimeHours"
              label="Total Time (Hours)"
              value={formState.totalTimeHours.length > 0 ? parseInt(formState.totalTimeHours) : null}
              onChange={handleInputChange}
              type="number"
            />
          </Grid.Col>
          <Grid.Col md={2} width={12}>
            <TextField
              name="totalTimeMinutes"
              label="Total Time (Minutes)"
              value={formState.totalTimeMinutes.length > 0 ? parseInt(formState.totalTimeMinutes) : null}
              onChange={handleInputChange}
              type="number"
            />
          </Grid.Col>          
        </Grid.Row>
        
      )}
      <Grid.Row className="m-2">
        <Grid.Col md={8} width={12}>
          <TextField
            sx={{ width: "85%" }}
            name="activityUrl"
            value={formState.activityUrl}
            label="Activity URL"
            onChange={handleInputChange}
            multiline
          />
        </Grid.Col>
      </Grid.Row>
      <Grid.Row className="m-2">
          <IconButton
            color="secondary"
            aria-label="Save Outdoor Activity"
            onClick={addOutdoorActivity}          >
            <Tooltip title="Save Outdoor Activity">
              <SaveIcon fontSize="large" />
            </Tooltip>
          </IconButton>
      </Grid.Row>
    </Grid>
  );
}
