import React from "react";
import { styled } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Collapse from "@mui/material/Collapse";
import IconButton from "@mui/material/IconButton";
import Grid from "@mui/material/Unstable_Grid2";
import Item from "@mui/material/ListItem";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";

import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

const bull = (
  <Box
    component="span"
    sx={{ display: "inline-block", mx: "2px", transform: "scale(0.8)" }}
  >
    •
  </Box>
);

const ExpandMore = styled((props) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

function AccordianCard() {
  return (
    <div>
      <h3>(to remove)</h3>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <Typography>to take out</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            En ese restaurante, sacan la basura cinco veces al día.
          </Typography>
          <Typography>
            At that restaurant they take out the trash five times a day.
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel2a-content"
          id="panel2a-header"
        >
          <Typography>to get out</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>Sacó un fajo de billetes del bolsillo.</Typography>
          <Typography>He got out a wad of bills out of his pocket.</Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel3a-content"
          id="panel3a-header"
        >
          <Typography>to get off</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>Saca la mochila del sofá.</Typography>
          <Typography>Get your backpack off the sofa.</Typography>
        </AccordionDetails>
      </Accordion>
    </div>
  );
}

function AccordianCard2() {
  return (
    <div>
      <h3>(to expel)</h3>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel4a-content"
          id="panel4a-header"
        >
          <Typography>to remove</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            Hay varias maneras de sacar una astilla con la ayuda de unas pinzas.
          </Typography>
          <Typography>
            There are several ways to remove a splinter with the aid of
            tweezers.
          </Typography>
        </AccordionDetails>
      </Accordion>
    </div>
  );
}

export default function Flashcard() {
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined">
        <React.Fragment>
          <CardContent>
            <Typography>Sacar</Typography>
            <Typography variant="h5" component="div">
              sah{bull}kar
            </Typography>
            <Typography variant="body2">to take out</Typography>
          </CardContent>
          <CardActions>
            <Button size="small">Learn More</Button>
            <ExpandMore
              expand={expanded}
              onClick={handleExpandClick}
              aria-expanded={expanded}
              aria-label="show more"
            >
              <ExpandMoreIcon />
            </ExpandMore>
          </CardActions>
          <Collapse in={expanded} timeout="auto" unmountOnExit>
            <CardContent>
              <Grid container spacing={2}>
                <Grid item xs={6}>
                  <Item>
                    <AccordianCard></AccordianCard>
                  </Item>
                </Grid>
                <Grid item xs={6}>
                  <Item>
                    <AccordianCard2></AccordianCard2>
                  </Item>
                </Grid>
              </Grid>
            </CardContent>
          </Collapse>
        </React.Fragment>
      </Card>
    </Box>
  );
}
