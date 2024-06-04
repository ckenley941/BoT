import React, { useState, useEffect } from "react";

import _ from "lodash";

import Select from "@mui/material/Select";
import Grid from "@mui/material/Unstable_Grid2";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import FormHelperText from "@mui/material/FormHelperText";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import TabPanel from "../controls/TabPanel.jsx";

import IconButton from "@mui/material/IconButton";
import SaveIcon from "@mui/icons-material/Save";
import AddItemToDropdownDialog from "../controls/AddItemToDropdownDialog";
import TextField from "@mui/material/TextField";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

import { getOutdoorsActivities } from "../../services/OutdoorsService.ts";

export default function AddOutdoorsActivity() {
  const [formState, setFormState] = useState({
    activityName: "",
    activityDate: "",
    geographicArea: "",
    activityLength: "",
    elevationGain: "",
    activityTimeHours: "",
    activityTimeMinutes: ""
  });
  const [outdoorsActivities, setOutdoorsActivities] = useState([]);

  useEffect(() => {
    loadOutdoorActivities();
  }, []);

  const loadOutdoorActivities = async () => {
    getOutdoorsActivities().then((response) => {
      setOutdoorsActivities(response.data);
    });
  };

  const handleInputChange = (e) => {
    let newState = { ...formState };
    const { name, value } = e.target;
    _.set(newState, name, value);
    setFormState(newState);
  };

  const addOutdoorsActivity = () => {
    if (isValid()) {
      // thought.jsonDetails.json = JSON.stringify(thought.jsonDetails.values);
      // var newThought = {
      //   ...thought,
      //   details: removeEmptyRows(thought.details),
      //   websiteLinks: removeEmptyRows(thought.websiteLinks),
      // }
      // insertThought(newThought).then((response) => {
      //   alert("Thought added");
      //   setThought({
      //     description: "",
      //     thoughtBucketId: 0,
      //     details: [],
      //     websiteLinks: [],
      //   });
      // });
    }
  };

  const isValid = () => {
    var msg = "";
    return true;
  };

  return (
    <Grid container spacing={2} className="m-2">
    <Grid xs={4}>
      <FormControl fullWidth>
        <InputLabel id="activity-name-select-label">
          Activity Name *
        </InputLabel>
        <Select
          labelId="activity-name-select-label"
          id="activity-name-select"
          name="activtyName"
          label="Activty Name"
          maxWidth
          value={formState.activityName}
          onChange={handleInputChange}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          {outdoorsActivities.map((oa, i) => (
            <MenuItem key={i} value={oa}>
              {oa}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Grid>
    <Grid xs={4}>
      <DatePicker />
    </Grid>
    <Grid xs={12}>
      <TextField
        sx={{ width: "75%" }}
        name="geographicArea"
        value={formState.geographicArea}
        label="Geographic Area"
        onChange={handleInputChange}
        multiline
      />
    </Grid>
    <Grid xs={1}>
      <TextField
        name="length"
        label="Length"
        value={Number(formState.length)}
        onChange={handleInputChange}
        type="number"
      />
    </Grid>
    <Grid xs={11}>
      <TextField
        sx={{ width: "26%" }}
        name="elevationGain"
        label="Elevation Gain (Ft)"
        value={Number(formState.elevationGain)}
        onChange={handleInputChange}
        type="number"
      />
    </Grid>
    <Grid xs={2}>
      <TextField
        name="activityTimeHours"
        label="Activity Time (Hours)"
        value={Number(formState.activityTimeHours)}
        onChange={handleInputChange}
        type="number"
      />
    </Grid>
    <Grid xs={2}>
      <TextField
        name="activityTimeMinutes"
        label="Activity Time (Minutes)"
        value={Number(formState.activityTimeMinutes)}
        onChange={handleInputChange}
        type="number"
      />
    </Grid>
  </Grid>
  );
}
