import { React, useState } from "react";

import { Grid } from "tabler-react";
import TextField from "@mui/material/TextField";
import Tooltip from "@mui/material/Tooltip";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";

import IconButton from "@mui/material/IconButton";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";

import InputAdornment from "@mui/material/InputAdornment";

export default function DetailRow(props) {
  return (
    <Grid container>
      <Grid.Row>
        <Grid.Col md={4} width={12}>
          <FormControl>
            <RadioGroup
              row
              aria-labelledby="text-type-radio-buttons-group-label"
              name="textType"
              value={props.selectedTextType}
              onChange={props.handleTextTypeChange}
            >
              <FormControlLabel
                value="PlainText"
                control={<Radio />}
                label="Plain Text"
              />
              <FormControlLabel
                value="Json"
                control={<Radio />}
                label="Table"
                disabled={props.jsonData === undefined}
              />
              <FormControlLabel
                value="Html"
                disabled
                control={<Radio />}
                label="HTML"
              />
            </RadioGroup>
          </FormControl>
          <IconButton
            color="secondary"
            aria-label="Add"
            onClick={
              props.selectedTextType === "PlainText"
                ? props.handleAddDetail
                : props.handleAddColumn
            }
          >
            <Tooltip
              title={`Add ${
                props.selectedTextType === "PlainText" ? "Detail" : "Column"
              }`}
            >
              <AddIcon fontSize="medium" />
            </Tooltip>
          </IconButton>
        </Grid.Col>
        <Grid.Col md={1} width={12}></Grid.Col>
      </Grid.Row>
      {props.selectedTextType === "PlainText" ? (
        <>
          {props.textData.map((t, i) => (
            <Grid.Row>
              <Grid.Col md={12} width={12}>
                <TextField
                  sx={{ mt: 1, width: "80%" }}
                  label={props.title}
                  multiline
                  name={i}
                  onChange={props.handleInputChange}
                  value={t}
                />
                <IconButton
                  name={i}
                  color="secondary"
                  aria-label="Delete"
                  onClick={props.handleDelete}
                >
                  <Tooltip title={`Delete ${props.title}`}>
                    <DeleteIcon fontSize="large" />
                  </Tooltip>
                </IconButton>
              </Grid.Col>
            </Grid.Row>
          ))}
        </>
      ) : (
        <>
          <Grid.Row>
            {props.jsonData &&
              props.jsonData.keys.map((t, i) => (
                <Grid.Col md={2} width={12} className="mb-2">
                  <TextField
                    name={i}
                    label={`Column ${i + 1}`}
                    onChange={props.handleJsonColumnChange}
                    value={t}
                    InputProps={{
                      endAdornment: (
                        <InputAdornment>
                          <IconButton
                            color="secondary"
                            aria-label="Delete"
                            onClick={() => {props.handleDeleteColumn(i)}}
                          >
                            <Tooltip title="Delete Column">
                              <DeleteIcon fontSize="small" />
                            </Tooltip>
                          </IconButton>
                        </InputAdornment>
                      ),
                    }}
                  />
                </Grid.Col>
              ))}
          </Grid.Row>
          <IconButton
            color="secondary"
            aria-label="Add"
            onClick={props.handleAddRow}
          >
            <Tooltip title={`Add Row`}>
              <AddIcon fontSize="medium" />
            </Tooltip>
          </IconButton>
          {props.jsonData &&
            props.jsonData.values.map((v, rowIdx) => (
              <Grid.Row className="mb-2">
                {props.jsonData.keys.map((t, colIdx) => (
                  <Grid.Col md={2} width={12} className="mb-2">
                    <TextField                    
                    name={`${colIdx}|${rowIdx}`}
                    label={`${colIdx % 6 === 0 ? `Row ${rowIdx + 1} ${t}` : `${t}`}`}
                    onChange={props.handleJsonRowChange}
                    value={v[`Column${colIdx + 1}`]}
                    InputProps={{
                      endAdornment: colIdx === props.jsonData.keys.length - 1 ? (
                        <InputAdornment>
                          <IconButton
                            color="secondary"
                            aria-label="Delete"
                            onClick={() => {props.handleDeleteRow(rowIdx)}}
                          >
                            <Tooltip title="Delete Row">
                              <DeleteIcon fontSize="small" />
                            </Tooltip>
                          </IconButton>
                        </InputAdornment>
                      ) : <></>,
                    }}
                  />
                    {/* <TextField
                      label={`${
                        colIdx % 6 === 0 ? `Row ${rowIdx + 1} ${t}` : `${t}`
                      }`}
                      name={`${colIdx}|${rowIdx}`}
                      onChange={props.handleJsonRowChange}
                      value={v[`Column${colIdx + 1}`]}
                    /> */}
                  </Grid.Col>
                ))}
              </Grid.Row>
            ))}
        </>
      )}
    </Grid>
  );
}
